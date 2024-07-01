Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

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
        Const VolumeLabel = "Kids videos"       ' Volume label for kids videos HDD
        Dim files() As String, unsafe As Integer = 0, maxFileLength As Integer = 0, maxFile As String = "", targetDrive = Nothing
        Dim drives = My.Computer.FileSystem.Drives      ' List all drives
        TextBox1.AppendText($"Removing strange characters from files{vbCrLf}")
        Try
            targetDrive = drives.First(Function(drive) drive.IsReady AndAlso drive.VolumeLabel = VolumeLabel)     ' get the Kids videos drive
        Catch
        End Try
        If targetDrive Is Nothing Then
            MsgBox($"Could not find a drive with volume label '{VolumeLabel}'", vbCritical + vbOKOnly, "Drive not found")
        Else
            ' Top level folders must start with uppercase
            Dim toplevel = Directory.GetDirectories(targetDrive.ToString, "*", SearchOption.TopDirectoryOnly)
            TextBox1.AppendText($"Processing {targetDrive}{VolumeLabel}: {toplevel.Count} top level folders{vbCrLf}")
            toplevel = Array.ConvertAll(toplevel, Function(x) Strings.Right(x, Len(x) - 3))     ' remove the drive letter
            Dim illegal = toplevel.Where(Function(x) Not (x = "$RECYCLE.BIN" Or (x(0) >= "A" And x(0) <= "Z"))).ToList
            If illegal.Any Then
                For Each ill In illegal
                    TextBox1.AppendText($"ERROR: Top level directory {ill} is illegal - must start upper case{vbCrLf}")
                Next
                Exit Sub
            End If
            ' Check for illegal characters
            files = GetFilesRecursively(targetDrive.Name, "*.mp4")         ' get list of all mp4 files
            TextBox1.AppendText($"{files.Length} files found{vbCrLf}")
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
                    TextBox1.AppendText($"Renaming {file} to {safefile}{vbCrLf}")
                End If
            Next
            TextBox1.AppendText($"{unsafe} unsafe filenames fixed{vbCrLf}")
            TextBox1.AppendText($"Longest file name was {maxFile} ({maxFileLength}){vbCrLf}")
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
