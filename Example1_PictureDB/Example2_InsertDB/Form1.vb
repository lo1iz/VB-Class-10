Imports System.Data
Imports System.Data.SqlClient

Public Class Form1

    Dim constr As String = "Server=(LocalDB)\MSSQLLocalDB;AttachDBFilename=D:\Northwind.mdf"
    Dim conn As New SqlConnection(constr)

    Private Sub showData()
        conn.Open()
        Dim sql As String = "select * from categories"
        Dim cmd As New SqlCommand(sql, conn)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim data As New DataSet()
        adapter.Fill(data, "category")
        DataGridView1.DataSource = data.Tables("category")
        conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        Dim sql As String = "insert into categories(categoryname,description)
                            values(@categoryname, @description)"
        Dim cmd As New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("categoryname", TextBox1.Text)
        cmd.Parameters.AddWithValue("description", TextBox2.Text)

        If cmd.ExecuteNonQuery = -1 Then
            MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้")
        Else
            MessageBox.Show("สามารถเพิ่มข้อมูลได้")
        End If
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        showData()
    End Sub
End Class
