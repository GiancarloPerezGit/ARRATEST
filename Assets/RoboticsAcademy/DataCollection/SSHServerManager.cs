using Renci.SshNet;
using UnityEngine;
using Renci.SshNet.Common;
using System.IO;

namespace RoboticsAcademy.DataCollection
{
    /// <summary>
    /// Static class for doing a variety of operations related to working with SSH servers.
    /// </summary>
    public static class SSHServerManager
    {
        // Current server client.
        static SftpClient sftp;

        /// <summary>
        /// Open a server from necessary data.
        /// </summary>
        public static void OpenServer(string host, int port, string user, string pass)
        {
            // Init sftp client and connect.
            sftp = new SftpClient(host, port, user, pass);
            sftp.Connect();

            // If not connected, output an error.
            if (!sftp.IsConnected)
            {
                Debug.LogError("You didn't specify correct information for the server! Check your Username, Password, HostName, or Port.");
            }
        }

        /// <summary>
        /// Close a server.
        /// </summary>
        public static void CloseServer()
        {
            // If connected, disconnect.
            if (sftp.IsConnected)
            {
                sftp.Disconnect();
            }
        }

        /// <summary>
        /// Create a folder within server.
        /// </summary>
        public static void CreateFolder(string path)
        {
            string cwd = sftp.WorkingDirectory;
            string[] directories = path.Split(Path.DirectorySeparatorChar);

            // Check if connected and a folder does not exist already.
            if (sftp.IsConnected && !FolderExists(path))
            {
                foreach (string d in directories)
                {
                    sftp.CreateDirectory(d);
                    sftp.ChangeDirectory(WinPathToLinux(Path.Combine(sftp.WorkingDirectory, d)));
                }
            }
            sftp.ChangeDirectory(cwd);
        }

        /// <summary>
        /// Check if a folder exists in a server.
        /// </summary>
        private static bool FolderExists(string path)
        {
            // get working directory and init output bool.
            string cwd = sftp.WorkingDirectory;
            bool directoryExists = false;

            try
            {
                // Try to change the directory to the new path and back to your working dir.
                sftp.ChangeDirectory(WinPathToLinux(path));
                directoryExists = true;
                sftp.ChangeDirectory(cwd);
            }
            catch (SftpPathNotFoundException)
            {
                // If error is recieved, directory does not exist.
                directoryExists = false;
            }

            // return bool.
            return directoryExists;
        }

        /// <summary>
        /// Set current working dir in server.
        /// </summary>
        public static void SetCurrentWorkingDir(string path)
        {
            // Check if connected and working dir isnt already set to new path.
            if (sftp.IsConnected && !sftp.WorkingDirectory.Equals(path))
            {
                if (!FolderExists(path)) CreateFolder(path);
                sftp.ChangeDirectory(WinPathToLinux(path));
            }
        }

        /// <summary>
        /// Upload a file to current working dir in server.
        /// </summary>
        public static void UploadFileToWorkingDir(string fullPathToFile)
        {
            // Check if connected and file exists in local directory.
            if (sftp.IsConnected && File.Exists(fullPathToFile))
            {
                // Open file in local dir and upload.
                using (var fileToUpload = File.OpenRead(fullPathToFile))
                {
                    sftp.UploadFile(fileToUpload, WinPathToLinux(Path.Combine(sftp.WorkingDirectory, Path.GetFileName(fullPathToFile))));
                }
            }
        }

        public static void UploadFileToDir(string fullPathToFile, string uploadPath)
        {
            string cwd = sftp.WorkingDirectory;
 
            // Check if connected and file exists in local directory.
            if (sftp.IsConnected && File.Exists(fullPathToFile))
            {
                if (!FolderExists(uploadPath))
                {
                    CreateFolder(uploadPath);
                }
                
                // Open file in local dir and upload.
                using (var fileToUpload = File.OpenRead(fullPathToFile))
                {
                    sftp.UploadFile(fileToUpload, WinPathToLinux(Path.Combine(uploadPath, Path.GetFileName(fullPathToFile))));
                }
            }
        }

        /// <summary>
        /// Convert windows paths to linux ones.
        /// </summary>
        public static string WinPathToLinux(string path)
        {
            return path.Replace('\\', '/');
        }
    }
}