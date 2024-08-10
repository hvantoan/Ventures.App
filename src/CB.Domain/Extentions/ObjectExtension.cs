﻿using Newtonsoft.Json;

namespace CB.Domain.Extentions;

public static class ObjectExtension {

    private static readonly JsonSerializerSettings jsonSetting = new() {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    public static T Clone<T>(this T obj) {
        string json = JsonConvert.SerializeObject(obj, jsonSetting);
        return JsonConvert.DeserializeObject<T>(json, jsonSetting)!;
    }
}
