using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserFactory 
{
    /// <summary>
    /// This games uses a factory for lasers to make use of object pooling.
    /// (We have many lasers :))
    /// </summary>
    static LaserFactory()
    {
        SceneManager.activeSceneChanged += (scene, scene2) => CreateLaserCache();
        CreateLaserCache();
    }

    private static void CreateLaserCache()
    {
        var resource = Resources.Load<GameObject>("Laser");
        prefab = resource;

        lasers.Clear();
        for (int i = 0; i < 1000; i++)
        {
            var laser = GameObject.Instantiate<GameObject>(resource);
            laser.SetActive(false);
            lasers.Add(laser);
        }
    }

    private static GameObject prefab;
    private static List<GameObject> lasers = new List<GameObject>(1000);
    public static List<GameObject> GetLasers() { return lasers; }

    public static GameObject CreateLaser(Blaster blaster)
    {
        GameObject laser = GetLaser();
        Vector3 dir = GetDirection(blaster);

        var laserComponent = laser.GetComponent<Laser>();
        laserComponent.Reset(blaster.GetLaserProperties(), dir);

        laser.transform.position = blaster.transform.position;
        laser.transform.rotation = blaster.transform.rotation;
        laser.SetActive(true);
        return laser;
    }

    private static Vector3 GetDirection(Blaster blaster)
    {
        Vector3 dir = Vector3.one ;
        switch (blaster.shotDirection)
        {
            case ShootDirections.Up: dir = blaster.transform.TransformDirection(Vector3.up); break;
            case ShootDirections.Down: dir = blaster.transform.TransformDirection(Vector3.down); break;
        }

        return dir;
    }

    private static GameObject GetLaser()
    {
        GameObject laser = null;
        GameObject current;
        for (int i = 0; i < lasers.Count && laser == null; i++)
        {
            current = lasers[i];
            if (!current.activeSelf)
                laser = current;
        }

        if (laser == null)
        {
            laser = GameObject.Instantiate<GameObject>(prefab);
            lasers.Add(laser);
        }

        return laser;
    }
}
