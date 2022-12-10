using System;
using System.Collections.Generic;
using MySqlConnector;
using Swashbuckle.AspNetCore.SwaggerGen;
using VoteWebAPI.Controllers;

namespace VoteWebAPI.Database
{
    public class Connectionhandler
    {
        private Connectionhandler() 
        {

        }

        private string ConnectionString = @"SERVER=voteapp-db,3306;DATABASE=VoteDB;UID=root;PWD=AppelCake69;";

        private static Connectionhandler instance;
        public static Connectionhandler GetInstance()
        {
            if(instance == null)
            {
                instance = new Connectionhandler();
            }
            return instance;
        }

        public void InformDB(string UID)
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(ConnectionString);

            try
            {
                cnn.Open();

                MySqlCommand command = new MySqlCommand($"INSERT IGNORE INTO Users (DeviceID) VALUES (@uid)", cnn);
                command.Parameters.AddWithValue("@uid", UID);
                command.ExecuteNonQuery();

                command.Dispose();
            }
            catch(MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public Question[] GetUnansweredQuestionsByUID(string UID)
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(ConnectionString);

            List<Question> Q = new List<Question>();

            try
            {
                cnn.Open();

                
                //(select * from Questions left join AnsweredQuestions on AnsweredQuestions.QuestionID = Questions.QuestionID left join Users on Users.userID = AnsweredQuestions.UserID where AnsweredQuestions.UserID = (select UserID from Users where UserID = "0A0027000015"))

                MySqlCommand command = new MySqlCommand(@"select QuestionId, Question, Options from Questions where QuestionID not in (select distinct Questions.QuestionID from Questions left join AnsweredQuestions on AnsweredQuestions.QuestionID = Questions.QuestionID left join Users on AnsweredQuestions.UserID = Users.UserID where Users.DeviceID = @uid)", cnn);
                command.Parameters.AddWithValue("@uid", UID);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Question Qu = new Question();
                    Qu._question = reader.GetString("Question");
                    Qu.options = reader.GetString("Options").Split('|');
                    Qu.questionid = reader.GetInt32("QuestionID");
                    Q.Add(Qu);
                }

                command.Dispose();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }

            return Q.ToArray();
        }

        ~Connectionhandler()
        {
            
        }
    }
}
