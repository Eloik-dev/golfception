using UnityEngine;

public class FollowUIPoint : MonoBehaviour
{
    [SerializeField] private Transform pointToFollow;
    [SerializeField][Range(0f, 10f)] private float followSpeed = 5f;
    [SerializeField][Range(0f, 10f)] private float lookSpeed = 5f;

    private void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            pointToFollow.position,
            followSpeed * Time.deltaTime
        );

        Vector3 dirToCam = Camera.main.transform.position - transform.position;
        if (dirToCam.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(-dirToCam);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                lookSpeed * Time.deltaTime
            );
        }
    }
}
