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

  const usuarioEstaLogado = TokenServico.usuarioAutenticado 
    ? TokenServico.usuarioAutenticado() 
    : TokenServico.obterToken() !== null;

  useEffect(() => {
    async function carregarDadosDoJogo() {
      try {
        setCarregando(true);
        const jogoIdNum = parseInt(id, 10);

        const [dadosJogo, todosGuias] = await Promise.all([
          JogoAPI.obterPorId(jogoIdNum),
          GuiaAPI.obterTodos()
        ]);

        const jogoFinal = Array.isArray(dadosJogo) ? dadosJogo[0] : dadosJogo;
        setJogo(jogoFinal);

        if (Array.isArray(todosGuias)) {
          const guiasFiltrados = todosGuias.filter(
            (g) => (g.jogoId || g.jogoID || g.JogoId) === jogoIdNum
          );
          setGuias(guiasFiltrados);
        }
      } catch (erro) {
        console.error('Erro ao carregar os detalhes do jogo:', erro);
      } finally {
        setCarregando(false);
      }
    }

    carregarDadosDoJogo();
  }, [id]);

  const handleSalvarGuia = (guia) => {
    const salvosAtuais = JSON.parse(localStorage.getItem('guiasSalvos') || '[]');
    const guiaIdTarget = guia.guiaId || guia.GuiaId || guia.id;

    const jaSalvo = salvosAtuais.some(
      (g) => (g.guiaId || g.GuiaId || g.id) === guiaIdTarget
    );

    if (!jaSalvo) {
      salvosAtuais.push(guia);
      localStorage.setItem('guiasSalvos', JSON.stringify(salvosAtuais));
      alert('Guia salvo no seu perfil com sucesso!');
    } else {
      alert('Este guia já está salvo no seu perfil!');
    }
  };

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

  const nomeJogo = jogo.nomeJogo || jogo.NomeJogo || jogo.nome || jogo.titulo;
  const capaJogo = jogo.capaJogo || jogo.CapaJogo || jogo.capa;

  return (
    <div className={styles.detalhesContainer}>
      <section className={styles.headerJogo}>
        <button className={styles.btnVoltar} onClick={() => navigate('/')}>
          ← Voltar
        </button>

        <div style={{ display: 'flex', gap: '16px', marginTop: '12px', alignItems: 'center' }}>
          {capaJogo && (
            <img
              src={capaJogo}
              alt={nomeJogo}
              style={{ width: '80px', height: '105px', objectFit: 'cover', borderRadius: '6px' }}
            />
          )}
          <div>
            <h1 style={{ color: '#fff', fontSize: '1.4rem', margin: '0 0 6px 0' }}>{nomeJogo}</h1>
            <p className={styles.descricaoJogo}>{jogo.descricao || jogo.Descricao || 'Sem descrição disponível.'}</p>
          </div>
        </div>
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
              <article key={guia.guiaId || guia.GuiaId || guia.id} className={styles.guiaCard}>
                <h3>{guia.titulo || guia.Titulo}</h3>
                <p>{guia.conteudo || guia.Conteudo}</p>

                {(guia.videoYoutube || guia.VideoYoutube) && (
                  <a
                    href={guia.videoYoutube || guia.VideoYoutube}
                    target="_blank"
                    rel="noreferrer"
                    className={styles.linkVideo}
                  >
                    📺 Assistir Tutorial no YouTube
                  </a>
                )}

                {usuarioEstaLogado && (
                  <div style={{ marginTop: '12px' }}>
                    <button
                      type="button"
                      onClick={() => handleSalvarGuia(guia)}
                      style={{
                        padding: '6px 12px',
                        backgroundColor: '#334155',
                        color: '#fff',
                        border: '1px solid #475569',
                        borderRadius: '6px',
                        cursor: 'pointer',
                        fontSize: '0.85rem'
                      }}
                    >
                      📌 Salvar Guia
                    </button>
                  </div>
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