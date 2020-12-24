using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using VideoLibrary;

namespace TelegramBotApp
{
    class Program
    {
        /// <summary>  
        /// Declare Telegrambot object  
        /// </summary>  
        private static readonly TelegramBotClient bot = new TelegramBotClient("1213789136:AAEwP0LpqiPL8IawjWCzzIZOE_Pnzq-n_tQ");
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
        private static void Csharpcornerbotmessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                PrepareQuestionnaires(e);
        }
        public static void PrepareQuestionnaires(MessageEventArgs e)
        {
            Console.WriteLine(e.Message.From.FirstName + ":" + e.Message.Text);
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
                //List<string> list = new List<string>() { "s", "S" };
                //List<Telegram.Bot.Types.Payments.LabeledPrice> price = new List<Telegram.Bot.Types.Payments.LabeledPrice>() { new Telegram.Bot.Types.Payments.LabeledPrice { Label = "label", Amount = 100 } };
                //bot.SendPollAsync(e.Message.Chat.Id, "salam", list, false);
                //bot.SendVenueAsync(e.Message.Chat.Id, 40, 40, "baki", "Address");
                MyRobat(e.Message.Chat.Id);
                if (e.Message.Text == "item")
                    bot.SendTextMessageAsync(e.Message.Chat.Id, "This is not a youtube link", replyMarkup: Lang);
            }
        }
        public static string SaveMp3(string url)
        {
            var source = @"C:\Users\Asus Tuf Gaming\source\repos\TelegramBotApp\TelegramBotApp\mp3\";
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
        //new
        public static void MyRobat(Telegram.Bot.Types.ChatId chat_id)
        {
            var rkm = new ReplyKeyboardMarkup();
            rkm.Keyboard =
         new KeyboardButton[][]
         {
        new KeyboardButton[]
        {
            new KeyboardButton("item"),
            new KeyboardButton("item")
        },
          new KeyboardButton[]
        {
            new KeyboardButton("item")
        }
         };
            bot.SendTextMessageAsync(chat_id, "Text", replyMarkup: rkm);
        }

        public static InlineKeyboardMarkup Lang = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
               new InlineKeyboardButton{Text="A",Url = "http://www.A.com/"},
               new InlineKeyboardButton(){Text="B",Url = "http://www.B.com/"}
            }
        });

    }
}
