using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;
public class movement : MonoBehaviour
{
    private bool iswaving;
   [SerializeField]private float _speed = 7f;
    [SerializeField]private float _mouseSensitivity = 50f;
    [SerializeField]private float _minCameraview = -70f, _maxCameraview = 80f;
    private CharacterController _charController;
    [SerializeField] private Camera _fppcamera;
        [SerializeField] private Camera _tppcamera;
        private Camera _camera =null;
        bool istpp;
          [SerializeField] private GameObject _tppc;
                    [SerializeField] private GameObject _fppc;



    private float xRotation = 0f;
         Vector3 moveVector;
         private bool iswalking;
 private Animator animator;
  float vertical,horizontal;

    // Start is called before the first frame update
    void Start()
    {
        iswaving=false;
        _charController = GetComponent<CharacterController>();
        _fppc.SetActive(true);
                _tppc.SetActive(false);

       _camera = _fppcamera;


       istpp=false;

        if(_charController == null)
        Debug.Log("No Character Controller Attached to Player");

        Cursor.lockState = CursorLockMode.Locked;
        animator= GetComponent<Animator>();
        iswalking=false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get WASD Input for Player
       // _camera.SetActive(true);
         vertical = Input.GetAxis("Vertical");
         horizontal = Input.GetAxis("Horizontal");
        //move player based on WASD Input
        Vector3 movement = transform.forward * vertical + transform.right * horizontal; //changed this line.
        _charController.Move(movement * Time.deltaTime * _speed);

       
        //Get Mouse position Input
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity; //changed this line.
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity; //changed this line.
          //Rotate the camera based on the Y input of the mouse
          xRotation -= mouseY;
          //clamp the camera rotation between 80 and -70 degrees
          xRotation = Mathf.Clamp(xRotation, _minCameraview, _maxCameraview);

          _camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
          //Rotate the player based on the X input of the mouse
          transform.Rotate(Vector3.up * mouseX * 3);
            
            moveVector = Vector3.zero;
 
         //Check if cjharacter is grounded
         if (_charController.isGrounded == false)
         {
             //Add our gravity Vector
             moveVector += Physics.gravity;
         }
 
         //Apply our move Vector , remeber to multiply by Time.delta
         _charController.Move(moveVector * Time.deltaTime);


         
        //float horizontal = Input.GetAxis("Horizontal");
            if(Input.GetKeyDown(KeyCode.C)){
            if(istpp==false){
            _camera=_tppcamera;
            _fppc.SetActive(false);
                _tppc.SetActive(true);
            istpp=true;
        }
        else if(istpp==true){
            _camera=_fppcamera;
            _fppc.SetActive(true);
                _tppc.SetActive(false);
            istpp=false;

        }
        
        }

        if(Input.GetKeyDown(KeyCode.T)){  
        iswaving=true;
        
        }
        if(Input.GetKeyDown(KeyCode.T)){  
        iswaving=false;
        }
        if(iswaving==true){
            animator.SetBool("iswaving",true);
       }
            if(iswaving==false){
                            animator.SetBool("iswaving",false);

       }
        }
          void FixedUpdate() {
             // _camera.SetActive(true);
               if(vertical>0||vertical<0||horizontal>0||horizontal<0){
                         animator.SetBool("isWalking",true);
              


            }
            else if(horizontal==0&&vertical==0){
             animator.SetBool("isWalking",false);

            }
            else{
                  animator.SetBool("isWalking",false);
            }

            
        
    }
    
}