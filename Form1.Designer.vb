<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        MenuStrip1 = New MenuStrip()
        DuplicateProgramFilesToolStripMenuItem = New ToolStripMenuItem()
        ShortcutsToFToolStripMenuItem = New ToolStripMenuItem()
        DuplicateProgramFilesToolStripMenuItem1 = New ToolStripMenuItem()
        RemoveStrangeCharactersToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {DuplicateProgramFilesToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(800, 24)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' DuplicateProgramFilesToolStripMenuItem
        ' 
        DuplicateProgramFilesToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ShortcutsToFToolStripMenuItem, DuplicateProgramFilesToolStripMenuItem1, RemoveStrangeCharactersToolStripMenuItem})
        DuplicateProgramFilesToolStripMenuItem.Name = "DuplicateProgramFilesToolStripMenuItem"
        DuplicateProgramFilesToolStripMenuItem.Size = New Size(73, 20)
        DuplicateProgramFilesToolStripMenuItem.Text = "File Check"
        ' 
        ' ShortcutsToFToolStripMenuItem
        ' 
        ShortcutsToFToolStripMenuItem.Name = "ShortcutsToFToolStripMenuItem"
        ShortcutsToFToolStripMenuItem.Size = New Size(216, 22)
        ShortcutsToFToolStripMenuItem.Text = "Shortcuts to F:"
        ' 
        ' DuplicateProgramFilesToolStripMenuItem1
        ' 
        DuplicateProgramFilesToolStripMenuItem1.Name = "DuplicateProgramFilesToolStripMenuItem1"
        DuplicateProgramFilesToolStripMenuItem1.Size = New Size(216, 22)
        DuplicateProgramFilesToolStripMenuItem1.Text = "Duplicate Program Files"
        ' 
        ' RemoveStrangeCharactersToolStripMenuItem
        ' 
        RemoveStrangeCharactersToolStripMenuItem.Name = "RemoveStrangeCharactersToolStripMenuItem"
        RemoveStrangeCharactersToolStripMenuItem.Size = New Size(216, 22)
        RemoveStrangeCharactersToolStripMenuItem.Text = "Remove strange characters"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "Form1"
        Text = "Form1"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents DuplicateProgramFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShortcutsToFToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DuplicateProgramFilesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents RemoveStrangeCharactersToolStripMenuItem As ToolStripMenuItem

End Class
