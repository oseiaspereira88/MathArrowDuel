using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

//Aluno: Oseias Pereira
//Matricula: 20190085186
//Link do Projeto: https://github.com/oseiaspereira88/MathArrowDuel.git
//Link do video: youtube.com/oseiaspereira88/...

public class TurnoScript : MonoBehaviour
{
    //Vars dos players
    private int vezDoPlayer = 1;
    private string nomeP1 = "Player1";
    private string nomeP2 = "Player2";
    private int nVidasP1 = 5;
    private int nVidasP2 = 5;
    private int ptsP1 = 0;
    private int ptsP2 = 0;

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
    public Text txtVezDoPlayer;
    public Text txtFim;
    public Button bComecar;
    private int turno = 0;
    private int meioTurno = 0;
    private bool terminoForcado = false;
    private int turnoTime;
    private string respostaP1;
    private string respostaP2;
    private string respostaCerta1;
    private string respostaCerta2;
    private float sp = 0;
    private bool isRunning = false;

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

    // Start is called before the first frame update
    void Start()
    {
        //preencher banco de perguntas
        perguntas = getPerguntas();
        sortPergunta();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (conteRegresiva() || terminoForcado)
            {
                terminoForcado = false;
                meioTurno++;
                if (meioTurno < 2)
                {
                    proximoMeioTurno();
                }
                else
                {
                    turno++;
                    proximoTurno();
                }

                checarVencedor();
            }
        }
    }

    public void sortPergunta()
    {
        int x = Random.Range(0, perguntas.Length);
        txtEnunciado.text = perguntas[x, 0];
        txtA.text = perguntas[x, 1];
        txtB.text = perguntas[x, 2];
        txtC.text = perguntas[x, 3];
        txtD.text = perguntas[x, 4];
        txtE.text = perguntas[x, 5];
        if (vezDoPlayer == 1) { respostaCerta1= perguntas[x, 6]; }
        else if (vezDoPlayer == 2) { respostaCerta2 = perguntas[x, 6]; }
        turnoTime = int.Parse(perguntas[x, 7]);
        txtTime.text = perguntas[x, 7];
    }

    public string[,] getPerguntas() {

        //padrão das perguntas::
        /////{"pergunta", "a", "b", "c", "d", "e", "resposta certa", "tempo limite"}
        
        string[,] perguntas = {
            { "Quanto é 4+7? ", "10", "14", "11", "13", "12", "11", "10" },
            { "Quanto é 4-7? ", "-5", "3", "-2", "4", "-3", "-3", "10" },
            { "Quanto é 3+9? ", "10", "14", "11", "13", "12", "12", "10" },
            { "Quanto é 3-9? ", "-5", "-6", "-2", "5", "6", "-6", "10" },
            { "Quanto é 5+4? ", "8", "9", "11", "12", "10", "9", "10" },
            { "Quanto é 5-4? ", "-1", "-2", "2", "-3", "1", "-1", "10" },
            { "Quanto é 8+9? ", "27", "16", "15", "17", "18", "17", "10" },
            { "Quanto é -25+8? ", "17", "-18", "16", "-19", "-17", "-17", "10" },
            { "Quanto é 4x7? ", "38", "27", "28", "26", "17", "28", "10" },
            { "Quanto é 56:7? ", "7", "4", "6", "8", "9", "8", "15" },
            { "Quanto é 3x9? ", "10", "14", "11", "13", "12", "12", "10" },
            { "Quanto é 3:9? ", "25", "26", "22", "27", "29", "27", "10" },
            { "Quanto é 5x4? ", "20", "40", "20", "44", "24", "20", "10" },
            { "Quanto é 5:4? ", "1.2", "1.12", "1.126", "1.4", "1.125", "1.125", "15" },
            { "Quanto é (-8)(-9)? ", "77", "72", "82", "-72", "-78", "72", "15" },
            { "Quanto é (-25):(8)? ", "3.125", "3.12", "3.126", "6.4", "3.2", "3.125", "15" },
            { "Quanto é 4+7? ", "10", "14", "11", "13", "12", "11", "10" },
            { "Quanto é 4-7? ", "-5", "3", "-2", "4", "-3", "-3", "10" },
            { "Quanto é 3+9? ", "10", "14", "11", "13", "12", "12", "10" },
            { "Quanto é 3-9? ", "-5", "-6", "-2", "5", "6", "-6", "10" },
            { "Quanto é 5+4? ", "8", "9", "11", "12", "10", "9", "10" },
            { "Quanto é 5-4? ", "-1", "-2", "2", "-3", "1", "-1", "10" },
            { "Quanto é 8+9? ", "27", "16", "15", "17", "18", "17", "10" },
            { "Quanto é -25+8? ", "17", "-18", "16", "-19", "-17", "-17", "10" },
            { "Quanto é 4x7? ", "38", "27", "28", "26", "17", "28", "10" },
            { "Quanto é 56:7? ", "7", "4", "6", "8", "9", "8", "15" },
            { "Quanto é 3x9? ", "10", "14", "11", "13", "12", "12", "10" },
            { "Quanto é 3:9? ", "25", "26", "22", "27", "29", "27", "10" },
            { "Quanto é 5x4? ", "20", "40", "20", "44", "24", "20", "10" },
            { "Quanto é 5:4? ", "1.2", "1.12", "1.126", "1.4", "1.125", "1.125", "15" },
            { "Quanto é (-8)(-9)? ", "77", "72", "82", "-72", "-78", "72", "15" },
            { "Quanto é (-25):(8)? ", "3.125", "3.12", "3.126", "6.4", "3.2", "3.125", "15" }
        };

        return perguntas;
    }

    public void getPergunta(int index)
    {
        //atualiza vars da pergunta
        txtEnunciado.text = perguntas[index, 0];
        txtA.text= perguntas[index, 1];
        txtB.text= perguntas[index, 2];
        txtC.text= perguntas[index, 3];
        txtD.text= perguntas[index, 4];
        txtE.text= perguntas[index, 5];
        txtTime.text = perguntas[index, 7];
        turnoTime = int.Parse(perguntas[index, 7]);

        if (vezDoPlayer == 1)
        {
            respostaCerta1= perguntas[index, 6];
        } else if (vezDoPlayer == 2)
        {
            respostaCerta2 = perguntas[index, 6];
        }
    }

    public void checkAcertos()
    {
        if(respostaP1 == respostaCerta1 && respostaP2 == respostaCerta2)
        {   //P1 acertou e P2 acertou
            diminuirVidaPlayer(1);
            diminuirVidaPlayer(2);
            addPontoPlayer(1);
            addPontoPlayer(2);
        } else if(respostaP1 == respostaCerta1)
        {   //P1 acertou e P2 errou
            addPontoPlayer(1);
            diminuirVidaPlayer(2);

        } else if (respostaP2 == respostaCerta2)
        {   //P2 acertou e P1 errou
            addPontoPlayer(2);
            diminuirVidaPlayer(1);
        } else
        {   
            //P1 errou e P2 errou
        }
    }

    public void diminuirVidaPlayer(int nPlayer)
    {
        if (nPlayer == 1)
        {
            bool isOver = false;
            for (int i = 0; i < vidasP1.Length; i++)
            {
                if (vidasP1[i].enabled && !isOver)
                {
                    vidasP1[i].enabled = false;
                    nVidasP1--;
                    isOver = true;
                }
            }
        }
        else if (nPlayer == 2)
        {
            bool isOver = false;
            for (int i = 0; i < vidasP2.Length; i++)
            {
                if (vidasP2[i].enabled && !isOver)
                {
                    vidasP2[i].enabled = false;
                    nVidasP2--;
                    isOver = true;
                }
            }
        }
    }

    public void addPontoPlayer(int nPlayer)
    {
        if (nPlayer == 1){ ptsP1++; txtPtsP1.text = ptsP1.ToString(); }
        else if (nPlayer == 2){ ptsP2++; txtPtsP2.text = ptsP2.ToString(); }
    }

    public void setEnabledAlternativas(bool enabled)
    {
        a.interactable = enabled;
        b.interactable = enabled;
        c.interactable = enabled;
        d.interactable = enabled;
        e.interactable = enabled;
    }

    public void selecteAlternativa(int selected)
    {
        a.enabled = selected != 1 ? false : true;
        b.enabled = selected != 2 ? false : true;
        c.enabled = selected != 3 ? false : true;
        d.enabled = selected != 4 ? false : true;
        e.enabled = selected != 5 ? false : true;
    }

    public void setOnAlternativas(bool on)
    {
        a.isOn = on;
        b.isOn = on;
        c.isOn = on;
        d.isOn = on;
        e.isOn = on;
    }

    public void checkAlternativas()
    {
        
        if (vezDoPlayer == 1)
        {
            if (a.isOn) { respostaP1 = perguntas[turno, 1]; }
            if (b.isOn) { respostaP1 = perguntas[turno, 2]; }
            if (c.isOn) { respostaP1 = perguntas[turno, 3]; }
            if (d.isOn) { respostaP1 = perguntas[turno, 4]; }
            if (e.isOn) { respostaP1 = perguntas[turno, 5]; }

        } else if (vezDoPlayer == 2)
        {
            if (a.isOn) { respostaP2 = perguntas[turno, 1]; }
            if (b.isOn) { respostaP2 = perguntas[turno, 2]; }
            if (c.isOn) { respostaP2 = perguntas[turno, 3]; }
            if (d.isOn) { respostaP2 = perguntas[turno, 4]; }
            if (e.isOn) { respostaP2 = perguntas[turno, 5]; }
        }
    }

    public bool checarVencedor()
    {
        bool have = false;
        if (turno-1>perguntas.Length/2) {
            //as perguntas se esgotaram 
            //conferir ponts
            if (ptsP1 > ptsP2)
            {
                have = true;
                txtFim.text = "Player1 venceu!";
            } else if(ptsP1 < ptsP2)
            {
                have = true;
                txtFim.text = "Player2 verceu!";
            }
            else
            {
                have = true;
                txtFim.text = "Temos um empate!";
            }

        }else if (nVidasP2 == 0)
        {
            have = true;
            txtFim.text = "Player1 venceu!";

        } else if(nVidasP1 == 0)
        {
            have = true;
            txtFim.text = "Player2 verceu!";
        }
        
        return have;
    }

    public bool conteRegresiva()
    {
        bool isFinish = false;

        sp += Time.deltaTime;

        if (sp >= 1 && turnoTime > 0)
        {
            turnoTime--;
            txtTime.text = turnoTime + "";
            sp = 0;
        }
        if(turnoTime <= 0)
        {
            isFinish = true;
        }

        return isFinish;
    }

    public void proximoTurno() {
        vezDoPlayer = 1;
        txtVezDoPlayer.text = "É a vez do Player" + vezDoPlayer;

        //pausa
        isRunning = false;
        bComecar.enabled = true;
    }

    public void proximoMeioTurno() {
        vezDoPlayer++;
        txtVezDoPlayer.text = "É a vez do Player" + vezDoPlayer;

        //pausa
        isRunning = false;
        bComecar.enabled = true;
    }

    public void terminarTurno() {
        terminoForcado = true;
    }

    public void bComercar() {
        //se o duelo já foi começado
        if (meioTurno==2 || turno>1) {
            isRunning = true;
            sortPergunta();
            bComecar.enabled = false;
        }
        else {
            isRunning = true;
            bComecar.enabled = false;
            txtVezDoPlayer.text = "É a vez do Player" + vezDoPlayer;
        }
        
    }


}
