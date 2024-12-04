using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitMover _navigator;
    private Picker _picker;

    private void Awake()
    {
        _picker = GetComponent<Picker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Base mainBase))
        {
            IResourceble resource = GetCurrentResource();

            if (resource != null)
            {
                mainBase.ActivateStorageResource(resource);

                DeliverResource();
            }
        }
    }

    public void Init(UnitMover navigator)
    {
        _navigator = navigator;
        _picker.SetUnitController(navigator);
    }

    public IResourceble GetCurrentResource()
    {
        PickingObject currentPickingObject = _picker.CurrentObject;

        if (currentPickingObject != null)
        {
            return currentPickingObject.GetComponent<IResourceble>();
        }

        return null;
    }

    public void DeliverResource()
    {
        PickingObject currentPickingObject = _picker.CurrentObject;

        if (currentPickingObject != null)
        {
            _picker.DropCurrentObject();

            if (currentPickingObject.TryGetComponent(out IDeathEvent deathEvent))
            {
                deathEvent.Die();
            }
        }
    }
}