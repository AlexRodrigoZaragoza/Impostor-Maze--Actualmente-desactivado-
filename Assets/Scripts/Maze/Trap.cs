using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField]
    private enum Tipo
    {
        salto,
        agacharse,
        izquierda,
        derecha
    }

    [SerializeField]
    private Tipo tipo;

    int wallCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controllTrap();
    }

    public void setTipo(int tipoNum)
    {
        switch (tipoNum){
            case 0:
                makeSaltoTrap();
                break;
            case 1:
                makeAgacharseTrap();
                break;
            case 2:
                makeIzTrap();
                break;
            case 3:
                makeDerTrap();
                break;
            default:
                makeSaltoTrap();
                break;
        }
    }

    void makeSaltoTrap()
    {
        tipo = Tipo.salto;
        transform.localScale = new Vector3(0.1f, 0.2f, 1f);
        gameObject.name = "SaltoTrap";
    }

    void makeAgacharseTrap()
    {
        tipo = Tipo.agacharse;
        transform.localScale = new Vector3(0.25f, 0.5f, 1f);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        gameObject.name = "AgacharseTrap";
    }

    void makeIzTrap()
    {
        tipo = Tipo.izquierda;
        transform.localScale = new Vector3(0.3f, 1f, 0.3f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.25f);
        gameObject.name = "IzTrap";
        controllTrap();
    }

    void makeDerTrap()
    {
        tipo = Tipo.derecha;
        transform.localScale = new Vector3(0.3f, 1f, 0.3f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.25f);
        gameObject.name = "DerTrap";
        controllTrap();
    }

    void controllTrap()
    {
        if(wallCount >= 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall" && (tipo == Tipo.izquierda || tipo == Tipo.derecha))
        {
            wallCount++;
        }else if(other.gameObject.name == "Start" || other.gameObject.name == "Finish")
        {
            Destroy(gameObject);
        }else if(other.gameObject.tag == "Player")
        {
            StartCoroutine(ServiceLocator.Instance.GetService<GameManager>().playerDead());
        }

    }

}
