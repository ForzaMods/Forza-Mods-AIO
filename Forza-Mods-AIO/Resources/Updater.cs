using System;
using Octokit;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Resources;

public class Updater : IDisposable
{
    private const string ProjectName = "Forza-Mods-AIO";
    private const string DestinationFileName = $"{ProjectName}-New.exe";
    private const string UpdaterBatchFileName = $"{ProjectName}-Updater.bat";
    private static readonly Version ToolVersion = Assembly.GetExecutingAssembly().GetName().Version!;
    
    private readonly GitHubClient _gitHubClient = new(new ProductHeaderValue(ProjectName));
    private IReadOnlyList<Release>? _releases;

    private const string Owner = "ForzaMods";
    private const string Repository = "AIO";

    public static async Task<bool> CheckInternetConnection()
    {
        const string url = "https://www.google.com/";
        const int timeoutMs = 2500;

        using var cts = new CancellationTokenSource(timeoutMs);
        using var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.GetAsync(url, cts.Token);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;            
        }
    }
    
    public async Task<bool> CheckForUpdates()
    {
        try
        {
            _releases = await _gitHubClient.Repository.Release.GetAll(Owner, Repository);
        }
        catch
        {
            MessageBox.Show(@"Github API Rate limit exceeded. Failed to check for updates.");
            return false;
        }

        if (_releases.Count <= 0)
        {
            return false;
        }
        
        var latestRelease = _releases[0];
        return new Version(latestRelease.TagName) > ToolVersion;
    }

    public async Task<bool> DownloadAndApplyUpdate()
    {
        if (_releases is not { Count: > 0 })
        {
            MessageBox.Show(@"The release count is 0. Failed to update the tool.");
            return false;
        }
        
        var latestRelease = _releases[0];

        var asset = latestRelease.Assets[0];
        var assetUrl = asset.BrowserDownloadUrl;

        HttpResponseMessage response;
        using (var httpClient = new HttpClient())
        {
            response = await httpClient.GetAsync(assetUrl);
        }
        
        if (response.IsSuccessStatusCode)
        {
            MessageBox.Show(@"Status code was not ok. Failed to update the tool.");
            return false; 
        }
        
        var destinationFile = Path.Combine(Directory.GetCurrentDirectory(), DestinationFileName);
        await using var fileStream = File.Create(destinationFile);
        await response.Content.CopyToAsync(fileStream);
        fileStream.Close();
        Update();
        
        return true;
    }

    private static void Update()
    {
        var path = Environment.ProcessPath;

        if (path == null)
        {
            return;
        }
        
        var splitPath = path.Split("\\");
        using (var writer = new StreamWriter(UpdaterBatchFileName))
        {
            writer.WriteLine("@echo off");
            writer.WriteLine("timeout 1 > nul");
            writer.WriteLine("del \"" + path + "\" ");
            writer.WriteLine("del \"" + splitPath[^1] + "\" ");
            writer.WriteLine($"ren {DestinationFileName} \"" + splitPath[^1] + "\" ");
            writer.WriteLine("start \"\" " + "\"" + splitPath[^1] + "\"");
            writer.WriteLine("goto 2 > nul & del \"%~f0\"");
        }
        
        var proc = new Process { StartInfo = 
        {
            FileName = UpdaterBatchFileName,
            WorkingDirectory = Environment.CurrentDirectory
        }};
        
        proc.Start();
        Environment.Exit(0);
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}