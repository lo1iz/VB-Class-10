Imports System.Data
Imports System.Data.SqlClient

Public Class Form1

    Dim constr As String = "Server=(LocalDB)\MSSQLLocalDB;AttachDBFilename=D:\Northwind.mdf"
    Dim conn As New SqlConnection(constr)

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn.Open()
        Dim sql As String = "select categoryname, description
                             from categories
                             where categoryid = " & TextBox1.Text

        Dim cmd As New SqlCommand(sql, conn)

        Dim adapter As New SqlDataAdapter(cmd)
        Dim data As New DataSet()
        adapter.Fill(data, "category")
        TextBox2.Text = data.Tables("category").Rows(0)("categoryname")
        TextBox2.Text = data.Tables("category").Rows(0)("description")
        conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        conn.Open()
        Dim sql As String = "update categories
                             set description = @descrip
                             where categoryid = @id"
        Dim cmd As New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("descrip", TextBox3.Text)
        cmd.Parameters.AddWithValue("id", TextBox1.Text)

        If cmd.ExecuteNonQuery() = -1 Then
            MessageBox.Show("ไม่สามารถแก้ไขข้อมูล")
        Else
            MessageBox.Show("แก้ไขข้อมูลเรียบร้อย")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox1.Select()
        End If
        conn.Close()

    End Sub
End Class
