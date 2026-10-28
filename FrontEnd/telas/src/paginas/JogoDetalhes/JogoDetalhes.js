import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { JogoAPI } from '../../Services/JogoAPI';
import { GuiaAPI } from '../../Services/GuiaAPI';
import { TokenServico } from '../../Services/TokenServico';
import styles from './JogoDetalhes.module.css';

export function JogoDetalhes() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [jogo, setJogo] = useState(null);
  const [guias, setGuias] = useState([]);
  const [carregando, setCarregando] = useState(true);

  const usuarioEstaLogado = TokenServico.estaAutenticado();

  useEffect(() => {
    async function carregarDadosDoJogo() {
      try {
        setCarregando(true);
        const jogoIdNum = parseInt(id, 10);

        const [dadosJogo, todosGuias] = await Promise.all([
          JogoAPI.obterPorId(jogoIdNum),
          GuiaAPI.obterTodos()
        ]);

        setJogo(dadosJogo);

        const guiasFiltrados = todosGuias.filter(
          (g) => g.jogoId === jogoIdNum || g.jogoID === jogoIdNum
        );
        setGuias(guiasFiltrados);
      } catch (erro) {
        console.error('Erro ao carregar os detalhes do jogo:', erro);
      } finally {
        setCarregando(false);
      }
    }

    carregarDadosDoJogo();
  }, [id]);

  if (carregando) {
    return (
      <div className={styles.detalhesContainer}>
        <h3>A carregar informações do jogo...</h3>
      </div>
    );
  }

  if (!jogo) {
    return (
      <div className={styles.detalhesContainer}>
        <h2>Jogo não encontrado!</h2>
        <button className={styles.btnVoltar} onClick={() => navigate('/')}>
          ← Voltar para o Catálogo
        </button>
      </div>
    );
  }

  return (
    <div className={styles.detalhesContainer}>
      <section className={styles.headerJogo}>
        <button className={styles.btnVoltar} onClick={() => navigate('/')}>
          ← Voltar
        </button>
        <h1>{jogo.nome || jogo.titulo}</h1>
        <p className={styles.descricaoJogo}>{jogo.descricao || 'Sem descrição disponível.'}</p>
      </section>

      <section className={styles.seccaoGuias}>
        <div className={styles.topoGuias}>
          <h2>Guias da Comunidade ({guias.length})</h2>
          {usuarioEstaLogado && (
            <button 
              className={styles.btnCriarGuia}
              onClick={() => navigate(`/jogo/${id}/novo-guia`)}
            >
              + Criar Novo Guia
            </button>
          )}
        </div>

        <div className={styles.listaGuias}>
          {guias.length > 0 ? (
            guias.map((guia) => (
              <article key={guia.guiaId || guia.id} className={styles.guiaCard}>
                <h3>{guia.titulo}</h3>
                <p>{guia.conteudo}</p>
                {guia.videoYoutube && (
                  <a 
                    href={guia.videoYoutube} 
                    target="_blank" 
                    rel="noreferrer"
                    className={styles.linkVideo}
                  >
                    📺 Assistir Tutorial no YouTube
                  </a>
                )}
              </article>
            ))
          ) : (
            <div className={styles.semGuias}>
              <p>Ainda não existem guias cadastrados para este jogo.</p>
            </div>
          )}
        </div>
      </section>
    </div>
  );
}