

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tcg
{
    public static class ResourceLoader
    {
        private static string GetJson (Resource resource)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string resourceName = resource switch
            {
                Resource.Cards => "tcg.Resources.Cards.json",
                _ => throw new NotImplementedException(),
            };

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) throw new FileNotFoundException();

            using StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();

            return json;
        }

        public static GameActionResult LoadJsonCards(string path, out List<ICard> cards)
        {
            cards = new List<ICard>();

            if (!File.Exists(path))
                return GameActionResult.JsonFilePathDoesNotExist;

            string json = File.ReadAllText(path);

            List<CardData> data;
            IParser parser = Parser.New();


            try
            {
                data = JsonConvert.DeserializeObject<List<CardData>>(json);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                return GameActionResult.JsonDeserializeError;
            }

            if (data == null)
                return GameActionResult.JsonDeserializeError;

            foreach (var d in data)
            {
                GameActionResult result = parser.CreateCardFromData(d, out ICard card);
                if (result != GameActionResult.Success)
                    return result;

                cards.Add(card);
            }

            return GameActionResult.Success;
        }

        public static List<ICard> LoadEmbeddedJsonCards ()
        {
            IParser parser = Parser.New();
            List<ICard> cards = new List<ICard>();
            string json = GetJson(Resource.Cards);
            List<CardData> cardData;

            try
            {
                cardData = JsonConvert.DeserializeObject<List<CardData>>(json) ?? new List<CardData>();
            }
            catch (Newtonsoft.Json.JsonException)
            {
                throw new Newtonsoft.Json.JsonException();
            }

            foreach (var data in cardData)
            {
                if (data == null) continue;
                
                var result = parser.CreateCardFromData(data,out var card);
                if (result != GameActionResult.Success) continue;
                cards.Add(card);
            }

            return cards;
        }

    }


    public enum Resource
    {
        Cards
    }
}