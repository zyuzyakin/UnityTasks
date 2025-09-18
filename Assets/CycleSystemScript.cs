using UnityEngine;

public class CicleSystemScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private int count;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private Directions direction;
    [SerializeField] private FillModes fillMode;
    [SerializeField] private float spacing;

    private int prevCount;
    private FillModes prevFillMode;
    private float prevSpacing;

    public int Count { get => count; set => count = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Radius { get => radius; set => radius = value; }
    public Directions Direction { get => direction; set => direction = value; }
    public FillModes FillMode { get => fillMode; set => fillMode = value; }
    public GameObject Prefab { get => prefab; set => prefab = value; }
    public float Spacing { get => spacing; set => spacing = value; }

    private GameObject[] clones;
    private float[] angles;

    public enum FillModes
    {
        fill, sequence
    }
    public enum Directions
    {
        clockwise, counterclockwise
    }

    private void Awake()
    {
        CreateObjects();
    }
    private void CreateObjects()
    {
        if (clones != null)
            foreach (var clone in clones)
                if (clone)
                    Destroy(clone);

        clones = new GameObject[count];
        angles = new float[count];

        prevCount = count;
        prevFillMode = fillMode;
        prevSpacing = spacing;

        for (int i = 0; i < count; i++)
        {
            float angle;
            if (fillMode == FillModes.fill)
            {
                angle = i * Mathf.PI * 2 / count;  // равномерное распределение по кругу
            }
            else
            {
                angle = i * spacing * Mathf.Deg2Rad;
            }
            angles[i] = angle;

            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * radius,
                0f,
                Mathf.Sin(angle) * radius
            );

            // создаем объект в позиции по окружности
            clones[i] = Instantiate(prefab, transform.position + pos, Quaternion.identity);

        }
    }
    void Update()
    {
        if (count != prevCount 
            || fillMode != prevFillMode
            || spacing != prevSpacing)
        {
            CreateObjects();
        }

        for (int i = 0; i < count; i++)
        {
            // Обновляем угол
            if (direction == Directions.counterclockwise)
                angles[i] += speed * Mathf.Deg2Rad * Time.deltaTime;
            else
            {
                angles[i] -= speed * Mathf.Deg2Rad * Time.deltaTime;
            }

                // Пересчитываем позицию
                Vector3 pos = new Vector3(
                    Mathf.Cos(angles[i]) * radius,
                    0f,
                    Mathf.Sin(angles[i]) * radius
                );

            // Обновляем позицию объекта
            
            clones[i].transform.position = transform.position + pos;
            

            Vector3 directionToCenter = new Vector3(0f, 0f, 0f) - clones[i].transform.position;
            clones[i].transform.rotation = Quaternion.LookRotation(-directionToCenter.normalized);
        }
    }
}
