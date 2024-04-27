Imports System.IO

Public Class Form1
    Private Sub ShortcutsToFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShortcutsToFToolStripMenuItem.Click
        ' Find any shortcuts on the desktop that refer to F:, so we can change them to D:
        Dim Shortcuts = System.IO.Directory.GetFiles("C:\Users\User\Desktop", "*.lnk")
        For Each shortcut In Shortcuts
            Dim shell = CreateObject("WScript.Shell")
            Dim path = shell.CreateShortcut(shortcut).TargetPath
            If UCase(path(0)) = "F" Then Debug.WriteLine(path)
        Next
        Debug.WriteLine($"{Shortcuts.Count} shortcuts processed")
    End Sub

    Private Sub DuplicateProgramFilesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DuplicateProgramFilesToolStripMenuItem1.Click
        ' Detect duplicate folders in D:\Program Files\ and D:\Program Files (x86). The x86 one is probably a leftover and can be removed.
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
End Class
