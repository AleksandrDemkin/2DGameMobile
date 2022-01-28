using UnityEngine;

public static class ResourceLoader
{
    public static GameObject LoadPrefab(ResourcePath path)
    {
        return Resources.Load<GameObject>(path.PathResource);
    }

    public static Sprite LoadSprite(ResourcePath path)
    {
        return Resources.Load<Sprite>(path.PathResource);
    }

    public static Rigidbody2D LoadObject<T>(ResourcePath path)
    {
        return Resources.Load<Rigidbody2D>(path.PathResource);
    }
}