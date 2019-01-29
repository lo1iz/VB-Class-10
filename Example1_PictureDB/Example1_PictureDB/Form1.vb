Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class Form1

    Dim conStr As String = "Server=(LocalDB)\MSSQLLocalDB;AttachDBFilename=D:\Northwind.mdf"
    Dim conn As New SqlConnection(conStr)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        conn.Open()

        Dim sql As String = "select picture from categories
                            where categoryid = " & TextBox1.Text
        Dim cmd As New SqlCommand(sql, conn)

        Dim adapter As New SqlDataAdapter(cmd)
        Dim data As New DataSet()
        adapter.Fill(data, "pic")

        Try
            Dim pic() As Byte = data.Tables("pic").Rows(0)("picture")
            Dim streamPicture As New MemoryStream(pic)
            streamPicture.Write(pic, 78, pic.Length - 78)
            PictureBox1.Image = Image.FromStream(streamPicture)
        Catch ex As Exception
            MessageBox.Show("ไม่พบข้อมูล")
            TextBox1.Clear()
            TextBox1.Select()
            PictureBox1.Image = Nothing

        End Try

        conn.Close()
    End Sub
End Class
