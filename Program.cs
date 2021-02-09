using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace FoE_Entity_Counter
{
   class Program
   {
      public static Dictionary<string, Tuple<int, List<string>>> counter = new Dictionary<string, Tuple<int, List<string>>>();
      static void Main(string[] args)
      {
         string content = File.ReadAllText($"{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "city.json")}");
         string allBuildings = File.ReadAllText($"{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "buildings.json")}");
         EntityList json = JsonConvert.DeserializeObject<EntityList>(content);
         EntityDefList def = JsonConvert.DeserializeObject<EntityDefList>(allBuildings);
         Entity[] entities = json.entities;
         for (int i = 0; i < entities.Length; i++)
         {
            Entity ent = entities[i];
            if (ent.cityentity_id.Contains("_Townhall")) continue;
            if (ent.cityentity_id.ToLower().Contains("street")) continue;
            if (ent.cityentity_id.ToLower().Contains("harbor")) continue;
            if (ent.cityentity_id.Contains("HubPart")) continue;
            if (ent.cityentity_id.Contains("IronAge_GuildExpedition")) continue;
            if (ent.cityentity_id.Contains("Ship1")) continue;
            if (ent.cityentity_id.Contains("IronAge_Battleground")) continue;
            if (ent.cityentity_id.Contains("FriendsTavern")) continue;
            if (ent.cityentity_id.Contains("FriendsTavern")) continue;
            EntityDefinition item = def.entities.ToList().Find(d => d.id == ent.cityentity_id);
            if (item != null)
            {
               if (counter.ContainsKey($"{item.length}x{item.width}"))
               {
                  counter[$"{item.length}x{item.width}"].Item2.Add(item.id);
                  counter[$"{item.length}x{item.width}"] = new Tuple<int, List<string>>(counter[$"{item.length}x{item.width}"].Item1 + 1, counter[$"{item.length}x{item.width}"].Item2);
               }
               else
               {
                  counter.Add($"{item.length}x{item.width}", new Tuple<int, List<string>>(1, new List<string>()));
                  counter[$"{item.length}x{item.width}"].Item2.Add(item.id);
               }
            }
         }
         if (counter.Count > 0)
         {
            foreach (KeyValuePair<string, Tuple<int, List<string>>> item in counter)
            {
               if (item.Value.Item1 > 3)
                  Console.WriteLine(item.Key + ": " + item.Value.Item1);
               else
               {
                  Console.WriteLine(item.Key + ": " + string.Join(", ",item.Value.Item2));
               }
            }
         }
         else
            Console.WriteLine("No Buildings found! Something went wrong!");
         Console.ReadKey();
      }
   }

   public class EntityList
   {
      public Entity[] entities { get; set; }
   }
   public class Entity
   {
      public int id { get; set; }
      public int player_id { get; set; }
      public string cityentity_id { get; set; }
      public string type { get; set; }
      public int x { get; set; }
      public int y { get; set; }
      public int connected { get; set; }
      public dynamic state { get; set; }
      public string __class__ { get; set; }
      public int level { get; set; }
      public int max_level { get; set; }
      public dynamic bonus { get; set; }
      public dynamic[] unitSlots { get; set; }
   }
   public class EntityDefList
   {
      public EntityDefinition[] entities { get; set; }
   }

   public class EntityDefinition
   {
      public string id { get; set; }
      public string asset_id { get; set; }
      public string name { get; set; }
      public string type { get; set; }
      public int width { get; set; }
      public int length { get; set; }
      public dynamic requirements { get; set; }
      public int construction_time { get; set; }
      public dynamic resaleResources { get; set; }
      public dynamic staticResources { get; set; }
      public bool is_special { get; set; }
      public int provided_happiness { get; set; }
      public bool is_multi_age { get; set; }
      public object[] entity_levels { get; set; }
      public dynamic[] abilities { get; set; }
      public string __class__ { get; set; }
   }
}
