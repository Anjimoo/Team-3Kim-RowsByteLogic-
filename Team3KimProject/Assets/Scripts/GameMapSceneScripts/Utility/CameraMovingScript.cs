using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraMovingScript : MonoBehaviour
{
    public float panSpeed = 10f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    public float scrollDistanceIncrement;
    public float maxScroll = 95;
    public float dragSpeed = 2f;
    private float stopSpeed = 1;
    int fov;
    public float rotateS=1.1f;
    int limit,limitcondition=0;
    


    public void Start()
    {

        limit = GameObject.Find("Map").GetComponent<TileMap>().mapGeneratorInfo.mapSizeX;
        if (limit>25)
        { limitcondition = 250; }
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w")) //|| Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") && pos.z >= -5.5 )//|| Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") )//|| Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") && pos.x >= 2)// || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        fov =(int) gameObject.GetComponent<Camera>().fieldOfView;
        pos.x = Mathf.Clamp(pos.x, -panLimit.x+(limit/2)-100/fov, panLimit.x+(limit/2)+100/fov);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y+8-limit/2/fov, panLimit.y+limitcondition/fov);

        transform.position = pos;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.fieldOfView > 30)
        {
            // Scroll camera inwards
            Camera.main.fieldOfView -= scrollDistanceIncrement;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.fieldOfView < maxScroll)
        {
            // Scrolling Backwards
            Camera.main.fieldOfView += scrollDistanceIncrement;
        }
        if (Input.GetMouseButton(2))
        {
            float speed = dragSpeed * Time.deltaTime;
            Camera.main.transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed * 10 * stopSpeed, 0, Input.GetAxis("Mouse Y") * speed * 10 * stopSpeed);
        }

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateS);


    }
}
