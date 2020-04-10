using System;
using System.Collections;
using System.Collections.Generic;
using CoreDui.Definitions;
using CoreDui.Enums;
using Newtonsoft.Json;

namespace CoreDui.JsonSerializers.Collection
{
    public class BaseCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ICollection<BaseCollectionModel>));
        }

        public override bool CanRead => false;
        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ICollection collection = value as ICollection;
            var objectList = new List<BaseCollectionModel>();
            if(collection != null)
            {
                foreach(BaseCollectionModel item in collection)
                {
                    objectList.Add(item);
                }
            }
            objectList.RemoveAll(o => o.Operation__ == ArrayItemOperation.Remove);
            objectList.ForEach(o =>
            {
                o.Operation__ = ArrayItemOperation.Update;
            });
            serializer.Serialize(writer, objectList);            
        }
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }

}
