using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour {
  //Objeto mira, Tamanho inicial da mira
  //e distancia maxima da mira em relação a camera
  public Transform reticle;
  private Vector3 startScale;
  public float MaxDistanceReticle;

  //Objeto alvo do raycast e
  //O componente RaycastTarget desse objeto
  private GameObject ActualTarget;
  public RaycastTarget ActualTargetEvents;

  //Compoente de controle da barra circular
  LoadCircle CircleScript;
  // Use this for initialization
  void Start () {
    //Inicializa a variavel StartScale usada para calcular o 
    //tamanho da mira em relação a distancia ao objeto que está na mira.
    float dist = Vector3.Distance(transform.position, reticle.position);	
    startScale = reticle.localScale / dist;

    //Pega o Componente LoadCircle desse objeto.
    CircleScript = GetComponent<LoadCircle>();
  }
    

  void Update () {
    //Raycast a partir desse objeto camera, 
    //na direção que o usuário está olhando
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;
    Physics.Raycast(ray, out hit);
    //Verifica o objeto que raycast colide
    if(hit.collider != null){
      //Verifica se o raycast mudou o objeto alvo
      if(ActualTarget != hit.collider.gameObject){
        ActualTarget = hit.collider.gameObject;
        //Verifica se tem um componente RaycastTarget nesse objeto
        if(ActualTarget.GetComponent<RaycastTarget>() != null){
          ActualTargetEvents = ActualTarget.GetComponent<RaycastTarget>();
          //Inicia o evento de carregamento da barra circular
          CircleScript.StartFill(ActualTargetEvents);
        }else{
          ActualTargetEvents = null;
          //Chama o evento de parar o carregamento da barra circular
          CircleScript.ResetFill(ActualTargetEvents);
        }
      }
    //Ajusta a posição, tamanho e rotação da mira
    // a partir do ponto de colisão do raycast
    reticle.position = hit.point;
    reticle.localScale = startScale * hit.distance;
    reticle.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
    //Se não estiver olhando para nenhum objeto
    } else {
      ActualTarget = null;
      //Chama o evento de parar o carregamento da barra circular
      CircleScript.ResetFill(ActualTargetEvents);
      //Ajusta a posição, tamanho e rotação da mira
      // em relação à distancia máxima 
      reticle.position = ray.GetPoint(MaxDistanceReticle);
      reticle.localScale = startScale * MaxDistanceReticle;
      reticle.rotation = Quaternion.FromToRotation(Vector3.forward, ray.direction);
    }
  }
}
