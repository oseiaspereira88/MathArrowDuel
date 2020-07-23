using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Aluno: Oseias Pereira
//Matricula: 20190085186
//Link do Projeto: https://github.com/oseiaspereira88/MathArrowDuel.git
//Link do video: youtube.com/oseiaspereira88/...

public class TurnoScript : MonoBehaviour
{
    //Vars dos players
    int vezDoPlayer = 1;
    string nomeP1 = "Player1";
    string nomeP2 = "Player2";
    int vidaP1 = 5;
    int vidaP2 = 5;
    int ptsP1 = 0;
    int ptsP2 = 0;

    //Vars status P1
    public Text txtNomeP1;
    public Text txtPtsP1;
    public Image[] vidasP1;
    public Image profileP1;
    
    //Vars status P2
    public Text txtNomeP2;
    public Text txtPtsP2;
    public Image[] vidasP2;
    public Image profileP2;

    //Vars turnos 
    public Text txtTime;
    public Text txtNTurno;
    int turno = 0;
    int turnoTime;
    float sp = 0;

    //Vars campos enunciado
    public Text txtEnunciado;
    public Text txtA;
    public Text txtB;
    public Text txtC;
    public Text txtD;
    public Text txtE;

    //Vars respostas checked
    public Toggle a;
    public Toggle b;
    public Toggle c;
    public Toggle d;
    public Toggle e;

    string[,] perguntas;

    public TurnoScript(int turnoTime)
    {
        this.turnoTime = turnoTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        //preencher banco de perguntas
        perguntas = getPerguntas();
        txtTime.text = perguntas[0, 7];
        turnoTime = int.Parse(perguntas[0, 7]);
    }

    // Update is called once per frame
    void Update()
    {
        sp += Time.deltaTime;

        if (sp>=1 && turnoTime > 0) {
            turnoTime--;
            txtTime.text = turnoTime + "";
            sp = 0;
        }
    }

    public string[,] getPerguntas() {
        string[,] perguntas = {
            { "Quanto é 4+7? ", "10", "14", "11", "13", "12", "11", "10" },
            { "Quanto é 3+9? ", "10", "14", "11", "13", "12", "12", "10" },
            { "Quanto é 5+4? ", "8", "9", "11", "12", "10", "9", "10" },
            { "Quanto é 4+7? ", "10", "14", "11", "13", "12", "11", "10" }
        };

        return perguntas;
    }

    public void proximoTurno() { }

    public void terminarTurno() { }

    public void checkResposta() { }


}
