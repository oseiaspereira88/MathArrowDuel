using System;
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
    public Button bPlay;
    private int turno = 1;
    private int meioTurno = 0;
    private bool isMeioTurnoFinish = false;
    private bool terminoForcado = false;
    private int turnoTime = 0;
    private string respostaP1;
    private string respostaP2;
    private string respostaCerta1;
    private string respostaCerta2;
    private string[] pergunta;
    private float sp = 0;
    private bool isRunning = false;
    private bool isPause = false;
    private bool haveResposta = false;
    private bool isDuelFinish = false;

    //Vars campos enunciado
    public Text txtEnunciado;
    public Text txtA;
    public Text txtB;
    public Text txtC;
    public Text txtD;
    public Text txtE;

    //Vars respostas checked
    public Button bA;
    public Button bB;
    public Button bC;
    public Button bD;
    public Button bE;

    //Componentes de Controle de Animação
    private CharacterController p1Controler, p2Controler;
    private Animator p1Animator, p2Animator;
    public GameObject player1, player2;

    
    private ArrayList perguntas;

    // Start is called before the first frame update
    void Start()
    {
        //preencher banco de perguntas
        perguntas = getPerguntas();
        resetAlternativas();

        //setandos os controladores de animação
        p1Controler = player1.GetComponent<CharacterController>();
        p2Controler = player2.GetComponent<CharacterController>();
        p1Animator = player2.GetComponent<Animator>();
        p2Animator = player2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (isMeioTurnoFinish || terminoForcado)
            {
                terminoForcado = terminoForcado ? false : false;
                meioTurno++;
                if (meioTurno == 1)
                {
                    proximoMeioTurno();
                }
                else
                {   if (!isDuelFinish)
                    {
                        checkAcertos();
                    }
                    if (!temosVencedor())
                    {
                        turno++;
                        proximoTurno();
                    }
                    else {
                        //é o fim do duelo
                    }
                }
            }
            else if(!terminoForcado && !isDuelFinish)
            {
                conteRegresiva();
            }   
        }

        if (isPause)
        {
            contePause(3);
        }
    }

    public void sortPergunta()
    {
        int x = UnityEngine.Random.Range(0, perguntas.Count - 1);
        pergunta = (string[])perguntas[x];
        txtEnunciado.text = pergunta[0];
        txtA.text = pergunta[1];
        txtB.text = pergunta[2];
        txtC.text = pergunta[3];
        txtD.text = pergunta[4];
        txtE.text = pergunta[5];
        if (vezDoPlayer == 1) { respostaCerta1= pergunta[6]; }
        else if (vezDoPlayer == 2) { respostaCerta2 = pergunta[6]; }
        turnoTime = int.Parse(pergunta[7]);
        txtTime.text = pergunta[7];
    }

    public ArrayList getPerguntas() {

        //padrão das perguntas::
        /////{"pergunta", "a", "b", "c", "d", "e", "resposta certa", "tempo limite"}
        ///
        var perguntas = new ArrayList();
        perguntas.Add(new string[]{ "Quanto é 4+7? ", "10", "14", "11", "13", "12", "11", "10" });
        perguntas.Add(new string[]{ "Quanto é 4-7? ", "-5", "3", "-2", "4", "-3", "-3", "10" });
        perguntas.Add(new string[]{ "Quanto é 3+9? ", "10", "14", "11", "13", "12", "12", "10" });
        perguntas.Add(new string[]{ "Quanto é 3-9? ", "-5", "-6", "-2", "5", "6", "-6", "10" });
        perguntas.Add(new string[]{ "Quanto é 5+4? ", "8", "9", "11", "12", "10", "9", "10" });
        perguntas.Add(new string[]{ "Quanto é 5-4? ", "-1", "-2", "2", "-3", "1", "-1", "10" });
        perguntas.Add(new string[]{ "Quanto é 8+9? ", "27", "16", "15", "17", "18", "17", "10" });
        perguntas.Add(new string[]{ "Quanto é (-25)+8? ", "17", "-18", "16", "-19", "-17", "-17", "10" });
        perguntas.Add(new string[]{ "Quanto é 4x7? ", "38", "27", "28", "26", "17", "28", "10" });
        perguntas.Add(new string[]{ "Quanto é 56:7? ", "7", "4", "6", "8", "9", "8", "15" });
        perguntas.Add(new string[]{ "Quanto é 3x9? ", "10", "14", "11", "13", "12", "12", "10" });
        perguntas.Add(new string[]{ "Quanto é 3x9-2? ", "25", "26", "22", "27", "29", "25", "10" });
        perguntas.Add(new string[]{ "Quanto é 5x4? ", "20", "40", "25", "44", "24", "20", "10" });
        perguntas.Add(new string[]{ "Quanto é 5:4? ", "1.2", "1.12", "1.126", "1.4", "1.125", "1.125", "15" });
        perguntas.Add(new string[]{ "Quanto é (-8)(-9)? ", "77", "72", "82", "-72", "-78", "72", "15" });
        perguntas.Add(new string[]{ "Quanto é (-25):(8)? ", "3.125", "3.12", "3.126", "6.4", "3.2", "3.125", "15" });
        
        perguntas.Add(new string[]{ "Quanto é 4+7? ", "10", "14", "11", "13", "12", "11", "10" });
        perguntas.Add(new string[]{ "Quanto é 4-7? ", "-5", "3", "-2", "4", "-3", "-3", "10" });
        perguntas.Add(new string[]{ "Quanto é 3+9? ", "10", "14", "11", "13", "12", "12", "10" });
        perguntas.Add(new string[]{ "Quanto é 3-9? ", "-5", "-6", "-2", "5", "6", "-6", "10" });
        perguntas.Add(new string[]{ "Quanto é 5+4? ", "8", "9", "11", "12", "10", "9", "10" });
        perguntas.Add(new string[]{ "Quanto é 5-4? ", "-1", "-2", "2", "-3", "1", "-1", "10" });
        perguntas.Add(new string[]{ "Quanto é 8+9? ", "27", "16", "15", "17", "18", "17", "10" });
        perguntas.Add(new string[]{ "Quanto é (-25)+8? ", "17", "-18", "16", "-19", "-17", "-17", "10" });
        perguntas.Add(new string[]{ "Quanto é 4x7? ", "38", "27", "28", "26", "17", "28", "10" });
        perguntas.Add(new string[]{ "Quanto é 56:7? ", "7", "4", "6", "8", "9", "8", "15" });
        perguntas.Add(new string[]{ "Quanto é 3x9? ", "10", "14", "11", "13", "12", "12", "10" });
        perguntas.Add(new string[]{ "Quanto é 3x9-2? ", "25", "26", "22", "27", "29", "25", "10" });
        perguntas.Add(new string[]{ "Quanto é 5x4? ", "20", "40", "25", "44", "24", "20", "10" });
        perguntas.Add(new string[]{ "Quanto é 5:4? ", "1.2", "1.12", "1.126", "1.4", "1.125", "1.125", "15" });
        perguntas.Add(new string[]{ "Quanto é (-8)(-9)? ", "77", "72", "82", "-72", "-78", "72", "15" });
        perguntas.Add(new string[]{ "Quanto é (-25):(8)? ", "3.125", "3.12", "3.126", "6.4", "3.2", "3.125", "15" });

        return perguntas;
    }

    public void pauseAnimation() { 
       //ataque do P1 e dano em P2
       
       //ataque de P2 e dano em p1
       
    }

    public void checkAcertos()
    {
        if (respostaP1 == respostaCerta1 && respostaP2 == respostaCerta2)
        {   //P1 acertou e P2 acertou
            diminuirVidaPlayer(1);
            diminuirVidaPlayer(2);
            addPontoPlayer(1);
            addPontoPlayer(2);

            //animações
            //p1Animator.SetBool("isAtaque", true);
            //p2Animator.SetBool("isDano", true);
            //pausa
            //p2Animator.SetBool("isAtaque", true);
            //p1Animator.SetBool("isDano", true);
        }
        else if (respostaP1 == respostaCerta1)
        {   //P1 acertou e P2 errou
            addPontoPlayer(1);
            diminuirVidaPlayer(2);

            //animações
            p1Animator.SetBool("isAtaque", true);
            p2Animator.SetBool("isDano", true);

        }
        else if (respostaP2 == respostaCerta2)
        {   //P2 acertou e P1 errou
            addPontoPlayer(2);
            diminuirVidaPlayer(1);

            //animações
            p2Animator.SetBool("isAtaque", true);
            p1Animator.SetBool("isDano", true);
        }
        else
        {
            //P1 errou e P2 errou
        }
    }

    public void diminuirVidaPlayer(int nPlayer)
    {
        if (nPlayer == 1)
        {
            bool isOver = false;
            for (int i = vidasP1.Length; i > 0; i--)
            {
                if (vidasP1[i-1].enabled && !isOver)
                {
                    vidasP1[i-1].enabled = false;
                    nVidasP1--;
                    isOver = true;
                }
            }
        }
        else if (nPlayer == 2)
        {
            bool isOver = false;
            for (int i = vidasP2.Length; i > 0; i--)
            {
                if (vidasP2[i-1].enabled && !isOver)
                {
                    vidasP2[i-1].enabled = false;
                    nVidasP2--;
                    isOver = true;
                }
            }
        }
    }

    public void addPontoPlayer(int nPlayer)
    {
        if (nPlayer == 1){ ptsP1++; txtPtsP1.text = "Pontos: " + ptsP1.ToString(); }
        else if (nPlayer == 2){ ptsP2++; txtPtsP2.text = "Pontos: " + ptsP2.ToString(); }
    }

    public void selecteAlternativa(int selected)
    {   
        if (vezDoPlayer == 1 && !terminoForcado && isRunning)
        {
            respostaP1 = pergunta[selected];
            setOnAlternativas(false, selected);
            isPause = true;
            haveResposta = true;
        }
        else if(vezDoPlayer == 2 && !terminoForcado && isRunning)
        {
            respostaP2 = pergunta[selected];
            setOnAlternativas(false, selected);
            //isPause = true;
            haveResposta = true;
        }
    }

    public void setOnAlternativas(bool on, int selected)
    {
        bA.interactable = selected != 1 ? on : !on;
        bB.interactable = selected != 2 ? on : !on;
        bC.interactable = selected != 3 ? on : !on;
        bD.interactable = selected != 4 ? on : !on;
        bE.interactable = selected != 5 ? on : !on;
    }

    public bool temosVencedor()
    {
        bool have = false;
        if (turno+1>perguntas.Count/2) {
            //as perguntas se esgotaram 
            //conferir ponts
            if (ptsP1 > ptsP2)
            {
                have = true;
                txtFim.text = "Player1 venceu!";
                isDuelFinish = true;
            } else if(ptsP1 < ptsP2)
            {
                have = true;
                txtFim.text = "Player2 verceu!";
                isDuelFinish = true;
            }
            else
            {
                have = true;
                txtFim.text = "Temos um empate!";
                isDuelFinish = true;
            }

        }else if (nVidasP2 == 0)
        {
            have = true;
            txtFim.text = "Player1 venceu!";

            //animação de morte
            p2Animator.SetBool("isMorte", true);

        } else if(nVidasP1 == 0)
        {
            have = true;
            txtFim.text = "Player2 verceu!";

            //animação de morte
            p1Animator.SetBool("isMorte", true);
        }
        
        return have;
    }

    public void conteRegresiva()
    {
        bPlay.interactable = false;
        sp += Time.deltaTime;

        if (sp >= 1 && turnoTime > 0)
        {
            turnoTime--;
            txtTime.text = turnoTime + "";
            sp = 0;
        }
        if (turnoTime == 0)
        {
            isMeioTurnoFinish = true;
            resetAlternativas();

            //Checa se no turno não teve resposta
            if (!haveResposta && vezDoPlayer==1) {
                respostaP1 = "Não respondeu";
            } else if (!haveResposta && vezDoPlayer == 2)
            {
                respostaP2 = "Não respondeu";
            }
        }
    }
    
    public void contePause(int segundos)
    {
        sp += Time.deltaTime;
        if (sp >= 1 && segundos > 0)
        {
            segundos--;
            sp = 0;
        }
        if (segundos == 0)
        {
            terminoForcado = true;
            turnoTime = 0;
            isMeioTurnoFinish = true;
            resetAlternativas();
            isPause = false;
        }
    }

    public void resetAlternativas() {
        setOnAlternativas(true, 0); //ativa interactable para todas as alternativas
        txtA.text = "Prepare-se!";
        txtB.text = "Prepare-se!";
        txtC.text = "Prepare-se!";
        txtD.text = "Prepare-se!";
        txtE.text = "Prepare-se!";
        txtEnunciado.text = "Sua questão é...";
        txtTime.text = "00";
    }

    public void proximoTurno() {
        vezDoPlayer = 1;
        txtNTurno.text = (turno < 10) ? ("0" + turno) : turno + "";
        txtVezDoPlayer.text = "É a vez do Player" + vezDoPlayer;
        isMeioTurnoFinish = false;
        haveResposta = false;
        meioTurno = 0;

        //pausa
        isRunning = false;
        bPlay.interactable = true;
    }

    public void proximoMeioTurno() {
        vezDoPlayer++;
        txtVezDoPlayer.text = "É a vez do Player" + vezDoPlayer;
        isMeioTurnoFinish = false;
        haveResposta = false;

        //pausa
        isRunning = false;
        bPlay.interactable = true;
    }

    public void terminarTurno() {
        terminoForcado = true;
        resetAlternativas();
    }

    public void play() {
        //se o duelo já foi começado
        if (meioTurno==2 || turno>1) {
            isRunning = true;
            terminoForcado = false;
            bPlay.interactable = false;
            sortPergunta();
            
        }
        else {
            isRunning = true;
            bPlay.interactable = false;
            sortPergunta();
            txtVezDoPlayer.text = "É a vez do Player" + vezDoPlayer;
        }
        
    }


}
