using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
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
            bot.OnMessage += Csharpcornerbotmessage;
            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();

        }

        /// <summary>  
        /// Handle bot webhook  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private static void Csharpcornerbotmessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                PrepareQuestionnaires(e);
        }
        public static void PrepareQuestionnaires(MessageEventArgs e)
        {
            SaveMp3(e.Message.Text);

            if (e.Message.Text.Contains("billie"))
            {
                bot.SendTextMessageAsync(e.Message.Chat.Id, "https://www.youtube.com/watch?v=V1Pl8CzNzCw");
                bot.SendTextMessageAsync(e.Message.Chat.Id, "https://www.youtube.com/watch?v=DyDfgMOUjCI");
                bot.SendTextMessageAsync(e.Message.Chat.Id, "https://www.youtube.com/watch?v=pbMwTqkKSps");
            }
            //if (e.Message.Text.ToLower() == "hi")
            //    bot.SendTextMessageAsync(e.Message.Chat.Id, "hello dude" + Environment.NewLine + "welcome to csharp corner chat bot." + Environment.NewLine + "How may i help you ?");
            //if (e.Message.Text.ToLower().Contains("know about"))
            //    bot.SendTextMessageAsync(e.Message.Chat.Id, "Yes sure..!!" + Environment.NewLine + "Mahesh Chand is the founder of C# Corner.Please go through for more detail." + Environment.NewLine + "https://www.c-sharpcorner.com/about");
            //if (e.Message.Text.ToLower().Contains("csharpcorner logo?"))
            //{
            //    bot.SendStickerAsync(e.Message.Chat.Id, "https://csharpcorner-mindcrackerinc.netdna-ssl.com/App_Themes/CSharp/Images/SiteLogo.png");
            //    bot.SendTextMessageAsync(e.Message.Chat.Id, "Anything else?");
            //}
            //if (e.Message.Text.ToLower().Contains("list of featured"))
            //    bot.SendTextMessageAsync(e.Message.Chat.Id, "Give me your profile link ?");
            //if (e.Message.Text.ToLower().Contains("here it is"))
            //    bot.SendTextMessageAsync(e.Message.Chat.Id, Environment.NewLine + "https://www.c-sharpcorner.com/article/getting-started-with-ionic-framework-angular-and-net-core-3/" + Environment.NewLine + Environment.NewLine +
            //        "https://www.c-sharpcorner.com/article/getting-started-with-ember-js-and-net-core-3/" + Environment.NewLine + Environment.NewLine +
            //        "https://www.c-sharpcorner.com/article/getting-started-with-vue-js-and-net-core-32/");
        }

        public static void SaveMp3(string url)
        {
            var source = @"source";
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);
            File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = source + vid.FullName };
            var outputFile = new MediaFile { Filename = $"{source + vid.FullName}.mp3" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);

                engine.Convert(inputFile, outputFile);
            }
        }
    }
}
