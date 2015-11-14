#region

using UnityEngine;

#endregion

public class Chunk : MonoBehaviour
{
    private int contiguousPits;
    public Elevation leftElevation;
    public int maxDifficulty = 10;
    private int maxHeigthRange = 2;
    public Elevation middleElevation;
    public Elevation rightElevation;

    public int leftHeight
    {
        get { return leftElevation.height; }
    }

    public int middleHeight
    {
        get { return middleElevation.height; }
    }

    public int rightHeight
    {
        get { return rightElevation.height; }
    }

    public Vector3 leftPosition
    {
        get { return leftElevation.transform.position; }
    }

    public Vector3 middlePosition
    {
        get { return middleElevation.transform.position; }
    }

    public Vector3 rightPosition
    {
        get { return rightElevation.transform.position; }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void create(int difficulty, int heightToMatch)
    {
        //Elevation Generation
        var leftRestriction = allowedRange(heightToMatch);
        if (heightToMatch == 0)
        {
            contiguousPits++;
        }
        createLeftElevation(leftRestriction).createMiddleElevation().createRightElevation();
        //Trap creation
        var maxTraps = 0;
    }

    private Chunk createLeftElevation(Vector2 restriction)
    {
        var height = createHeight(restriction);
        if (height == 0)
        {
            contiguousPits++;
        }
        leftElevation.setHeight(height);
        return this;
    }

    private Chunk createMiddleElevation()
    {
        var height = createHeight(allowedRange(leftHeight));
        if (height == 0)
        {
            contiguousPits++;
            if (contiguousPits > 2)
            {
                height = 1;
                contiguousPits = 0;
            }
        }
        middleElevation.setHeight(height);
        return this;
    }

    private Chunk createRightElevation()
    {
        var height = createHeight(allowedRange(middleElevation.height));
        if (height == 0)
        {
            contiguousPits++;
            if (contiguousPits > 2)
            {
                height = 1;
                contiguousPits = 0;
            }
        }
        rightElevation.setHeight(height);
        return this;
    }

    private int createHeight(Vector2 restriction)
    {
        return Random.Range((int) restriction.x, (int) restriction.y + 1);
    }

    private Vector2 allowedRange(int height)
    {
        var upperLimit = Mathf.Min(3, height + 2);
        var lowerLimit = Mathf.Max(0, height - 2);
        return new Vector2(lowerLimit, upperLimit);
    }
}