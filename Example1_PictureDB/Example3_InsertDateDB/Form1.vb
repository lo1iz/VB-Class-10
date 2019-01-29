Imports System.Data
Imports System.Data.SqlClient

Public Class Form1

    Dim constr As String = "Server=(LocalDB)\MSSQLLocalDB;AttachDBFilename=D:\Northwind.mdf"
    Dim conn As New SqlConnection(constr)

    Private Sub showData()
        conn.Open()
        Dim sql As String = "select firstname, lastname, birthdate from employees"
        Dim cmd As New SqlCommand(sql, conn)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim data As New DataSet()
        adapter.Fill(data, "employee")
        DataGridView1.DataSource = data.Tables("employee")
        conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        Dim sql As String = "insert into employees(firstname,lastname,birthdate)
                             values(@firstname, @lastname, @birthdate)"
        Dim cmd As New SqlCommand(sql, conn)

        cmd.Parameters.AddWithValue("firstname", TextBox1.Text)
        cmd.Parameters.AddWithValue("lastname", TextBox2.Text)

        Dim birthdate As String = DateTimePicker1.Value.Year & "/" &
                                  DateTimePicker1.Value.Month & "/" &
                                  DateTimePicker1.Value.Day
        cmd.Parameters.AddWithValue("birthdate", birthdate)

        cmd.ExecuteNonQuery()

        conn.Close()
        showData()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        showData()
    End Sub
End Class
