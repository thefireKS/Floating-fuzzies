using UnityEngine;

public class Helper : MonoBehaviour
{
    [SerializeField] private Transform itemsHolderTransform;
    private GameObject _target;

    [Space(5)]
    [SerializeField] private float catchTime = 2f;
    private float _timeSinceLastCatch = Mathf.Infinity;

    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    private bool CanCatch() => _timeSinceLastCatch >= catchTime;

    private void Catch()
    {
        if (!CanCatch()) return;
        _target.gameObject.transform.LookAt(gameObject.transform);
        Destroy(_target, 2f);
        ChangeRotation();
        _timeSinceLastCatch = 0;
    }

    private void ChangeRotation()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation = new Quaternion(0, lookRotation.y, 0, lookRotation.w);
        transform.rotation = lookRotation;
    }

    private void Update()
    {
        _timeSinceLastCatch += Time.fixedDeltaTime;
        if (itemsHolderTransform.childCount > 0) _target = itemsHolderTransform.GetChild(0).gameObject;
        _animator.SetBool("IsAttacking", _target != null);
        if (_target != null)
        {
            Catch();
        }
    }
}
