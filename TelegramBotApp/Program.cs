using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using VideoLibrary;

namespace TelegramBotApp
{
    class Program
    {
        /// <summary>  
        /// Declare Telegrambot object  
        /// </summary>  
        private static readonly TelegramBotClient bot = new TelegramBotClient("{token}");

        /// <summary>  
        /// csharp corner chat bot web hook  
        /// </summary>  
        /// <param name="args"></param>  
        static void Main(string[] args)
        {
            Console.WriteLine();
            bot.OnMessage += Csharpcornerbotmessage;
            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }

        private static void Csharpcornerbotmessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                PrepareQuestionnaires(e);
        }
        public static void PrepareQuestionnaires(MessageEventArgs e)
        {
            Console.WriteLine("user :" + e.Message.Text);
            try
            {
                string networkpath = SaveMp3(e.Message.Text);
                InputOnlineFile videoFile = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(networkpath)));
                bot.SendVideoAsync(e.Message.Chat.Id, videoFile);
                if (File.Exists(networkpath))
                {
                    File.Delete(networkpath);
                }
            }
            catch (Exception)
            {
                bot.SendTextMessageAsync(e.Message.Chat.Id, "This is not a youtube link");
            }
        }
        public static string SaveMp3(string url)
        {
            var source = @"{path}";
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);
            File.WriteAllBytes(source + vid.FullName, vid.GetBytes());
            var input = new MediaFile { Filename = source + vid.FullName };
            var output = new MediaFile { Filename = $"{source + vid.FullName}.mp3" };
            //using (var engine = new Engine())
            //{
            //    engine.GetMetadata(input);
            //    engine.Convert(input, output);
            //}
            return source + vid.FullName;
        }
    }
}
