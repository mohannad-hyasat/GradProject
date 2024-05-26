using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class JumpscareManager : MonoBehaviour
{

    public GameObject Ghost;
    public Transform[] GhostJS;
    public Animator Anim;
    public float AnimationDuration = 5f;
    public bool IsAnim = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(Ghost, GhostJS[0]);
            AnimatorPlayer();
            if(!IsAnim)
            {
                Ghost.SetActive(false);
            }
        }

        













    }

    private async void AnimatorPlayer()
    {
        IsAnim = true;
        float timeLapsed = 0f;
        while (timeLapsed < AnimationDuration)
        {
            timeLapsed += Time.deltaTime;
            await Task.Yield();
        }
        IsAnim = false;
        




    }















}
