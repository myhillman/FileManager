Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form1
    Private Sub ShortcutsToFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShortcutsToFToolStripMenuItem.Click
        ' Find any shortcuts on the desktop that refer to F:, so we can change them to D:
        Dim Shortcuts = System.IO.Directory.GetFiles("C:\Users\User\Desktop", "*.lnk")
        For Each shortcut In Shortcuts
            Dim shell = CreateObject("WScript.Shell")
            Dim path = shell.CreateShortcut(shortcut).TargetPath
            If UCase(path(0)) = "F" Then Debug.WriteLine(path)
        Next
        Debug.WriteLine($"{Shortcuts.Length} shortcuts processed")
    End Sub

    Private Sub DuplicateProgramFilesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DuplicateProgramFilesToolStripMenuItem1.Click
        ' Detect duplicate folders in D:\Program Files\ and D:\Program Files (x86)\. The x86 one is probably a leftover and can be removed.
        Dim PFfolders As New List(Of String), PF86folders As New List(Of String)

        Dim di As New DirectoryInfo("D:\Program Files\")
        Dim PF = di.GetDirectories
        For Each f In PF
            PFfolders.Add(Strings.Replace(f.FullName, di.FullName, ""))
        Next

        di = New DirectoryInfo("D:\Program Files (x86)\")
        Dim PF86 = di.GetDirectories
        For Each f In PF86
            PF86folders.Add(Strings.Replace(f.FullName, di.FullName, ""))
        Next

        Dim both = PFfolders.Intersect(PF86folders)
        For Each D In both.ToList
            Debug.WriteLine(D)
        Next
    End Sub

    Private Sub RemoveStrangeCharactersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveStrangeCharactersToolStripMenuItem.Click
        ' Remove strange characters from files on "Kids videos" drive
        Const VolumeLabel = "Kids videos"       ' Volume lable for kids videos HDD
        Dim files() As String, unsafe As Integer = 0, maxFileLength As Integer = 0, maxFile As String = "", targetDrive = Nothing
        Dim drives = My.Computer.FileSystem.Drives      ' List all drives

        Try
            targetDrive = drives.First(Function(drive) drive.IsReady AndAlso drive.VolumeLabel = VolumeLabel)     ' get the Kids videos drive
        Catch
        End Try
        If targetDrive Is Nothing Then
            MsgBox($"Could not find a drive with volume label '{VolumeLabel}'", vbCritical + vbOKOnly, "Drive not found")
        Else
            files = GetFilesRecursively(targetDrive.Name, "*.mp4")         ' get list of all mp4 files
            Debug.WriteLine($"{files.Length} files found")
            For Each file In files
                ' Remember longest file name. I think the limit is 127
                If file.Length > maxFileLength Then
                    maxFileLength = file.Length
                    maxFile = file
                End If
                Dim safefile = Regex.Replace(file, "[^ -\~]", "")      ' remove any illegal characters
                safefile = Regex.Replace(safefile, "\s{2,}", " ")      ' replace multiple spaces with one
                If file <> safefile Then
                    unsafe += 1
                    safefile = Path.GetFileName(safefile)
                    My.Computer.FileSystem.RenameFile(file, safefile)      ' rename the file
                    Debug.WriteLine($"Renaming {file} to {safefile}")
                End If
            Next
            Debug.WriteLine($"{unsafe} unsafe filenames fixed")
            Debug.WriteLine($"Longest file name was {maxFile} ({maxFileLength})")
        End If
    End Sub
    Public Shared Function GetFilesRecursively(path As String, searchPattern As String) As String()
        Dim filePaths As New List(Of String)(Directory.GetFiles(path, searchPattern))

        For Each folderPath In Directory.GetDirectories(path)
            Try
                filePaths.AddRange(GetFilesRecursively(folderPath, searchPattern))
            Catch ex As UnauthorizedAccessException
                'Ignore inaccessible folders
            End Try
        Next
        Return filePaths.ToArray()
    End Function
    Public Iterator Function EnumerateFilesRecursively(path As String, searchPattern As String) As IEnumerable(Of String)
        For Each filePath In Directory.EnumerateFiles(path, searchPattern)
            Yield filePath
        Next
        For Each folderPath In Directory.EnumerateDirectories(path)
            Try
                For Each filePath In EnumerateFilesRecursively(folderPath, searchPattern)
                    Yield filePath
                Next
            Catch ex As UnauthorizedAccessException
                'Ignore inaccessible folders
            End Try
        Next
    End Function
End Class
