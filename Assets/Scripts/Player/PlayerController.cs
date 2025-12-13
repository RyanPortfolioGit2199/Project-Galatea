using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement Parameters")]
    [SerializeField] float turnSpeed;
    [SerializeField] float moveSpeed;
    [Tooltip("Will need to change moveSpeed variable later on to incorperate the calculation from the thruster upgrades to affect player speed when player upgrades are implemented")]

    [Header("Movement Debug Visualization (Don't Change!)")]
    [SerializeField] Vector2 moveValue;
    InputAction moveAction;

    Scene currentScene;
    int UpgradeScene = 2; // Change if the UpgradeScene gets changed in the Scene List
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        
        
            MouseLook();
            PlayerMovement();
        
        
    }

    private void MouseLook()
    {
        if(currentScene.buildIndex == UpgradeScene) return;// makes it sure that the player can move in the Upgrade menu.

        //see if I can make this calculate first using FixedUpdate causing glitches when moving mouse position rotating isnt tracking properly when player is moving.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        }
    }

    private void PlayerMovement()
    {
        if(currentScene.buildIndex == UpgradeScene) return;// makes it sure that the player can move in the Upgrade menu.

        moveValue = moveAction.ReadValue<Vector2>();



        transform.Translate(Vector3.forward * moveValue.y * moveSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * moveValue.x * moveSpeed * Time.deltaTime, Space.World);

        /*
         * // need to fix the movement when moving left and right for it to not rotate around the mouse position I just want to strafe left and right.
         * Fix is using Space.World to make the Player move along the World's X-axis instead of the player's X-axis to stop rotating around of the mouse position.
        */
    }
}
