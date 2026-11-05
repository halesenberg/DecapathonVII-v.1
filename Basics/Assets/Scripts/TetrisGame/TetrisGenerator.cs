using UnityEngine;

public class TetrisShapeGenerator : MonoBehaviour
{
    public GameObject cubePrefab;

    void Start()
    {
        CreateIShape(new Vector3(-15, 0, 0));
        CreateOShape(new Vector3(-10, 0, 0));
        CreateTShape(new Vector3(-5, 0, 0));
        CreateSShape(new Vector3(0, 0, 0));
        CreateZShape(new Vector3(5, 0, 0));
        CreateJShape(new Vector3(10, 0, 0));
        CreateLShape(new Vector3(15, 0, 0));
    }

    GameObject CreateIShape(Vector3 position)
    {
        return CreateShape("I_Shape", position, new Vector3[] {
            new Vector3(-1.5f,0,0),
            new Vector3(-0.5f,0,0),
            new Vector3(0.5f,0,0),
            new Vector3(1.5f,0,0)
        });
    }

    GameObject CreateOShape(Vector3 position)
    {
        return CreateShape("O_Shape", position, new Vector3[] {
            new Vector3(-0.5f,0.5f,0),
            new Vector3(0.5f,0.5f,0),
            new Vector3(-0.5f,-0.5f,0),
            new Vector3(0.5f,-0.5f,0)
        });
    }

    GameObject CreateTShape(Vector3 position)
    {
        return CreateShape("T_Shape", position, new Vector3[] {
            new Vector3(-1f,0,0),
            new Vector3(0f,0,0),
            new Vector3(1f,0,0),
            new Vector3(0f,1f,0)
        });
    }

    GameObject CreateSShape(Vector3 position)
    {
        return CreateShape("S_Shape", position, new Vector3[] {
            new Vector3(0f,0,0),
            new Vector3(1f,0,0),
            new Vector3(-1f,1f,0),
            new Vector3(0f,1f,0)
        });
    }

    GameObject CreateZShape(Vector3 position)
    {
        return CreateShape("Z_Shape", position, new Vector3[] {
            new Vector3(-1f,0,0),
            new Vector3(0f,0,0),
            new Vector3(0f,1f,0),
            new Vector3(1f,1f,0)
        });
    }

    GameObject CreateJShape(Vector3 position)
    {
        return CreateShape("J_Shape", position, new Vector3[] {
            new Vector3(1f,1f,0),
            new Vector3(-1f,0,0),
            new Vector3(-0.5f,0,0),
            new Vector3(0.5f,0,0)
        });
    }

    GameObject CreateLShape(Vector3 position)
    {
        return CreateShape("L_Shape", position, new Vector3[] {
            new Vector3(-0.5f,1.5f,0),
            new Vector3(-0.5f,0.5f,0),
            new Vector3(-0.5f,-0.5f,0),
            new Vector3(0.5f,-0.5f,0)
        });
    }

    GameObject CreateShape(string name, Vector3 position, Vector3[] blocksPositions)
    {
        GameObject parent = new GameObject(name);
        parent.transform.position = position;

        foreach (var pos in blocksPositions)
        {
            GameObject cube = Instantiate(cubePrefab, position + pos, Quaternion.identity);
            cube.transform.parent = parent.transform;
        }

        return parent;
    }
}
