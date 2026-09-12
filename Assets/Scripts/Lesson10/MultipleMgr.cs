using System.Collections.Generic;
using UnityEngine;

public class MultipleMgr
{
    private static MultipleMgr instance = new MultipleMgr();
    public static MultipleMgr Instance => instance;
    Dictionary<string,Dictionary<string,Sprite>> multipleDic = new Dictionary<string, Dictionary<string,Sprite>>();
    private MultipleMgr() {}

    public Sprite GetSprite(string multipleName,string spriteName)
    {
        if (multipleDic.ContainsKey(multipleName))
        {
            if (multipleDic[multipleName].ContainsKey(spriteName))
            {
                return multipleDic[multipleName][spriteName];
            }
        }
        else
        {
            Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
            Sprite[] sprites = Resources.LoadAll<Sprite>(multipleName);
            foreach (Sprite sprite in sprites)
            {
                spriteDict.Add(sprite.name, sprite);
            }
            multipleDic.Add(multipleName, spriteDict);
            return spriteDict[spriteName];
        }
        return null;
    }

    public void ClearDic()
    {
        multipleDic.Clear();
        Resources.UnloadUnusedAssets();
    }
}
