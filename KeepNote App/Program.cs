
using KeepNote_Sql;
using System;
using System.Data;
using System.Data.SqlClient;

namespace KeepNote_Sql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Data Source=US-FGRQ8S3;Initial Catalog=TakeNote;User Id =sa;password=Lakshmiprabha@2001");
            Note note = new Note();
            string res = "";
            do
            {
                Console.WriteLine(" KeepNote App");
                Console.WriteLine("1.CreateNote");
                Console.WriteLine("2.ViewNote");
                Console.WriteLine("3.ViewAllNotes");
                Console.WriteLine("4.UpdateNote");
                Console.WriteLine("5.DeleteNote");
                Console.WriteLine("Enter your choice");
                int choice = Convert.ToInt32(Console.ReadLine());



                switch (choice)
                {
                    case 1:
                        {
                            note.CreateNote(con);
                            break;
                        }
                    case 2:
                        {
                            note.ViewNote(con);
                            break;
                        }
                    case 3:
                        {
                            note.ViewAllNotes(con);
                            break;
                        }
                    case 4:
                        {
                            note.UpdateNote(con);
                            break;
                        }
                    case 5:
                        {
                            note.DeleteNote(con);
                            break;
                        }
                }
                Console.WriteLine("Do u want to continue[y/n]");
                res = Console.ReadLine();
            } while (res.ToLower() == "y");

        }
        class Note
        {
            public void CreateNote(SqlConnection con)
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from notes", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                var row = ds.Tables[0].NewRow();

                Console.WriteLine("Enter Title: ");
                row["title"] = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Enter Description: ");
                row["description"] = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Enter Date: ");
                row["dates"] = Convert.ToDateTime(Console.ReadLine());

                ds.Tables[0].Rows.Add(row);

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
                Console.WriteLine("Note created successfully");

            }
            public void ViewNote(SqlConnection con)
            {
                Console.WriteLine("Enter id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                SqlDataAdapter adp = new SqlDataAdapter($"select * from notes where id = {id}", con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "notetable");
                for (int i = 0; i < ds.Tables["notetable"].Rows.Count; i++)
                {
                    Console.WriteLine("id--title--description--date");
                    for (int j = 0; j < ds.Tables["notetable"].Columns.Count; j++)
                    {
                        Console.Write($"{ds.Tables["notetable"].Rows[i][j]} -- ");
                    }
                    Console.WriteLine();
                }
            }
            public void ViewAllNotes(SqlConnection con)
            {
                SqlDataAdapter adp = new SqlDataAdapter("select * from notes", con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "notetable");
                for (int i = 0; i < ds.Tables["notetable"].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables["notetable"].Columns.Count; j++)
                    {
                        Console.Write($"{ds.Tables["notetable"].Rows[i][j]} --");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine($"Total Notes are: {ds.Tables["notetable"].Rows.Count}");
            }
            public void UpdateNote(SqlConnection con)
            {
                Console.WriteLine("Enter the id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                SqlDataAdapter adp = new SqlDataAdapter($"select * from notes where id ={id}", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                Console.WriteLine("Enter the column name to update: ");
                string colname = Console.ReadLine();

               Console.WriteLine("Enter the index row to update: ");
                int index = Convert.ToInt32(Console.ReadLine());

               Console.WriteLine("Enter the updated value:");
                string value = Console.ReadLine();

               ds.Tables[0].Rows[index][colname] = value;

                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                Console.WriteLine("Note updated successfully");
            }
            public void DeleteNote(SqlConnection con)
            {
                SqlDataAdapter adp = new SqlDataAdapter($"select * from notes", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

               Console.WriteLine("Enter the row want to delete:");
                int row = Convert.ToInt32(Console.ReadLine());

                ds.Tables[0].Rows[row].Delete();

                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                Console.WriteLine("TableNotes deleted successfully");
            }
        }
    }
}

    
