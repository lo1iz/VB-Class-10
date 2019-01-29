Imports System.Data
Imports System.Data.SqlClient

Public Class Form1

    Dim constr As String = "Server=(LocalDB)\MSSQLLocalDB;AttachDBFilename=D:\Northwind.mdf"
    Dim conn As New SqlConnection(constr)

    Private Sub showData()
        conn.Open()
        Dim sql As String = "select * from categories"
        Dim cmd As New SqlCommand(sql, conn)
        Dim adap As New SqlDataAdapter(cmd)
        Dim data As New DataSet()
        adap.Fill(data, "category")
        DataGridView1.DataSource = data.Tables("category")
        conn.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        showData()
        DataGridView1.RowHeadersVisible = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
        DataGridView1.MultiSelect = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If DataGridView1.SelectedRows.Count = 0 Then
                MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการจะลบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim result As DialogResult = MessageBox.Show("ต้องการลบข้อมูลหรือไม่ ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    conn.Open()
                    Dim key As Integer
                    key = DataGridView1.SelectedRows.Item(0).Cells("categoryid").Value
                    Dim sql As String = "delete from categories
                             where categoryid = " & key
                    Dim cmd As New SqlCommand(sql, conn)

                    If cmd.ExecuteNonQuery = -1 Then
                        MessageBox.Show("ไม่สามารถลบข้อมูล", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show("ลบข้อมูลสำเร็จ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                    conn.Close()
                    showData()
                Else
                    MessageBox.Show("ยกเลิกการลบข้อมูล", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Dim message = String.Format("Error : {0}", ex.Message)
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class