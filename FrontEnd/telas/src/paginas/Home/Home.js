import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { JogoAPI } from '../../Services/JogoAPI';
import './Home.module.css';

export function Home() {
  const [jogos, setJogos] = useState([]);
  const [carregando, setCarregando] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    async function carregarJogos() {
      try {
        const resposta = await JogoAPI.obterTodos();
        setJogos(resposta);
      } catch (erro) {
        console.error('Erro ao carregar jogos:', erro);
      } finally {
        setCarregando(false);
      }
    }

    carregarJogos();
  }, []);

  const handleSelecionarJogo = (jogoId) => {
    navigate(`/jogo/${jogoId}`);
  };

  if (carregando) {
    return <div className="home-container"><h3>A carregar jogos...</h3></div>;
  }

  return (
    <div className="home-container">
      <section className="hero-section">
        <h1>Selecione um Jogo 🎮</h1>
        <p>Escolha um jogo abaixo para ver e criar guias da comunidade.</p>
      </section>

      <section className="seccao">
        <h2>Catálogo de Jogos</h2>
        <div className="grid-cards">
         {jogos.map((jogo) => (
          <div
            key={jogo.jogoId || jogo.id}
            className="card card-clicavel"
            onClick={() => handleSelecionarJogo(jogo.jogoId || jogo.id)}
          >
            {(jogo.capaJogo || jogo.capa) && (
              <img 
                src={jogo.capaJogo || jogo.capa} 
                alt={jogo.nomeJogo || jogo.nome} 
                style={{ width: '100%', height: '200px', objectFit: 'cover', borderRadius: '8px 8px 0 0' }}
              />
            )}

            <h3>{jogo.nomeJogo || jogo.nome || jogo.titulo}</h3>
            <p>{jogo.descricao || 'Clique para ver os guias deste jogo.'}</p>
            <span className="btn-ver-guias">Ver Guias →</span>
          </div>
        ))}
        </div>
      </section>
    </div>
  );
}