using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ContainerReader
{
    public class container
    {
        public static string Read(string args)
        {
            try
            {
                // Open a filestream with the user selected file
                FileStream file = new FileStream(args, FileMode.Open);

                // Create a binary reader that will be used to read the file
                BinaryReader reader = new BinaryReader(file);

                // Grab the type, currently the only supported type is 0xD
                int type = reader.ReadInt32();

                // Throw generic exception
                if (type != 0xd && type != 0xe)
                {
                    throw new Exception();
                }

                // Could be something other than int32, but very unlikely
                int numFiles = reader.ReadInt32();

                BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32());

                string[] name = BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32()).Split('!');

                //Console.WriteLine("Package Full Name: " + name[0]);
                //Console.WriteLine("Id: " + name[1]);

                // Not awfully sure what this is, so I'll just skip past it until I can figure out what it is. Possibly title ID or other internal data
                reader.ReadBytes(0xc);

                // Not sure what this is either, but I'll print it
                BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32());

                if (type == 0xe)
                {
                    // 8 padding bytes
                    reader.ReadBytes(8);
                }

                // Loop through every file in the index, and print info about it
                for (int i = 0; i < numFiles; i++)
                {
                    string fileName = BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32());

                    string secondName = BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32());

                    // Unknown value, surrounded by quotes for some reason
                    string UnknownValue = BinaryReaderHelper.ReadUnicodeString(reader, reader.ReadInt32());

                    byte containerNum = reader.ReadByte();

                    // Unknown, will add later if it is important
                    reader.ReadBytes(4);

                    // The guid folder that the files reside in
                    byte[] guid1 = reader.ReadBytes(4);
                    Array.Reverse(guid1);
                    byte[] guid2 = reader.ReadBytes(2);
                    Array.Reverse(guid2);
                    byte[] guid3 = reader.ReadBytes(2);
                    Array.Reverse(guid3);
                    byte[] guid4 = reader.ReadBytes(2);
                    byte[] guid5 = reader.ReadBytes(6);

                    // More unknown data... really gotta try to figure this out
                    reader.ReadBytes(0x18);

                    // holy unwieldy code batman...gotta condense this sometime
                    // TODO: condense this
                    string guid = BitConverter.ToString(guid1).Replace("-", string.Empty) + "-" + BitConverter.ToString(guid2).Replace("-", string.Empty) + "-" + BitConverter.ToString(guid3).Replace("-", string.Empty) + "-" + BitConverter.ToString(guid4).Replace("-", string.Empty) + "-" + BitConverter.ToString(guid5).Replace("-", string.Empty);

                    //Console.WriteLine(fileName + " | " + UnknownValue + " | " + containerNum + " | " + guid);

                    // Go through every sub files
                    try
                    {
                        if (type == 0xe) // (1 file = a block lenght 160)
                        {
                            string subContainerName = Path.Combine(guid.Replace("-", string.Empty).ToUpper(), "container." + containerNum);

                            // Open a filestream with the user selected file
                            FileStream subFile = new FileStream(Path.Combine(Path.GetDirectoryName(args), subContainerName), FileMode.Open);

                            // Create a binary reader that will be used to read the file
                            BinaryReader subReader = new BinaryReader(subFile);

                            // Grab the type, currently the only supported type is 0xD
                            int subType = subReader.ReadInt32();

                            // Throw generic exception
                            if (subType != 0x04)
                            {
                                throw new Exception();
                            }

                            int subNumFiles = subReader.ReadInt32();

                            for (int y = 0; y < subNumFiles; y++)
                            {
                                // subFileName has a fixed length
                                string subfileName = BinaryReaderHelper.ReadUnicodeString(subReader, 0x40);
                                subfileName = subfileName.Replace("\0", string.Empty);

                                // The sub guid folder that the files reside in
                                byte[] subGuid1 = subReader.ReadBytes(4);
                                Array.Reverse(subGuid1);
                                byte[] subGuid2 = subReader.ReadBytes(2);
                                Array.Reverse(subGuid2);
                                byte[] subGuid3 = subReader.ReadBytes(2);
                                Array.Reverse(subGuid3);
                                byte[] subGuid4 = subReader.ReadBytes(2);
                                byte[] subGuid5 = subReader.ReadBytes(6);

                                // The second sub guid folder that the files reside in
                                byte[] subGuid6 = subReader.ReadBytes(4);
                                Array.Reverse(subGuid6);
                                byte[] subGuid7 = subReader.ReadBytes(2);
                                Array.Reverse(subGuid7);
                                byte[] subGuid8 = subReader.ReadBytes(2);
                                Array.Reverse(subGuid8);
                                byte[] subGuid9 = subReader.ReadBytes(2);
                                byte[] subGuid10 = subReader.ReadBytes(6);

                                string subGuid = BitConverter.ToString(subGuid1).Replace("-", string.Empty) + BitConverter.ToString(subGuid2).Replace("-", string.Empty) + BitConverter.ToString(subGuid3).Replace("-", string.Empty) +BitConverter.ToString(subGuid4).Replace("-", string.Empty) + BitConverter.ToString(subGuid5).Replace("-", string.Empty);
                                // The second guid is the same
                                string subSecondGuid = BitConverter.ToString(subGuid6).Replace("-", string.Empty) + BitConverter.ToString(subGuid7).Replace("-", string.Empty) + BitConverter.ToString(subGuid8).Replace("-", string.Empty) + BitConverter.ToString(subGuid9).Replace("-", string.Empty) + BitConverter.ToString(subGuid10).Replace("-", string.Empty);
                                if (subfileName == "ProfileData")
                                {
                                    file.Close();
                                    file.Dispose();
                                    subFile.Close();
                                    subReader.Dispose();
                                    subFile.Dispose();
                                    if (subGuid != subSecondGuid)
                                        return Path.Combine(guid.Replace("-", string.Empty).ToUpper(), subSecondGuid);
                                    else
                                        return Path.Combine(guid.Replace("-", string.Empty).ToUpper(), subGuid);
                                }


                                //Console.WriteLine("\t" + subfileName + " | " + subGuid);
                            }

                            subReader.Dispose();
                            subFile.Dispose();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("This sub type is not supported yet.");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("File could not be accessed!");
            }
            catch (Exception)
            {
                Console.WriteLine("This type is not supported yet.");
            }
            return "";
        }
    }
}