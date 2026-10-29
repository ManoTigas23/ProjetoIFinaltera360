import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { GuiaAPI } from '../../Services/GuiaAPI';
import { TokenServico } from '../../Services/TokenServico';
import styles from './Perfil.module.css';

export function Perfil() {
  const navigate = useNavigate();
  const [meusGuias, setMeusGuias] = useState([]);
  const [guiasSalvos, setGuiasSalvos] = useState([]);
  const [carregando, setCarregando] = useState(true);

  useEffect(() => {
    async function carregarDadosPerfil() {
      try {
        setCarregando(true);
        const usuarioId = TokenServico.obterUsuarioId ? TokenServico.obterUsuarioId() : localStorage.getItem('usuarioId');

        const todosGuias = await GuiaAPI.obterTodos();
        if (Array.isArray(todosGuias)) {
          const publicados = todosGuias.filter(
            (g) => (g.usuarioId || g.UsuarioId) == usuarioId
          );
          setMeusGuias(publicados);
        }

        const salvosStorage = JSON.parse(localStorage.getItem('guiasSalvos') || '[]');
        setGuiasSalvos(salvosStorage);

      } catch (erro) {
        console.error('Erro ao carregar dados do perfil:', erro);
      } finally {
        setCarregando(false);
      }
    }

    carregarDadosPerfil();
  }, []);

  return (
    <div className={styles.perfilContainer}>
      <header className={styles.headerPerfil}>
        <div>
          <h2>Meu Perfil 👤</h2>
          <p>Gerencie as suas publicações e conta</p>
        </div>
        <button className={styles.btnSair} onClick={() => {
          localStorage.clear();
          navigate('/login');
        }}>
          Sair da Conta
        </button>
      </header>

      <section className={styles.seccao}>
        <h3>Meus Guias Publicados ({meusGuias.length})</h3>
        <hr />
        {meusGuias.length > 0 ? (
          <div className={styles.gridGuias}>
            {meusGuias.map((guia) => (
              <div key={guia.guiaId || guia.GuiaId} className={styles.cardGuia}>
                <h4>{guia.titulo || guia.Titulo}</h4>
                <p>{guia.conteudo || guia.Conteudo}</p>
                <button onClick={() => navigate(`/jogo/${guia.jogoId || guia.JogoId}`)}>
                  Ver no Jogo →
                </button>
              </div>
            ))}
          </div>
        ) : (
          <p className={styles.semConteudo}>Você ainda não publicou nenhum guia.</p>
        )}
      </section>

      <section className={styles.seccao} style={{ marginTop: '40px' }}>
        <h3>Guias Salvos 📌 ({guiasSalvos.length})</h3>
        <hr />
        {guiasSalvos.length > 0 ? (
          <div className={styles.gridGuias}>
            {guiasSalvos.map((guia) => (
              <div key={guia.guiaId || guia.GuiaId} className={styles.cardGuia}>
                <h4>{guia.titulo || guia.Titulo}</h4>
                <p>{guia.conteudo || guia.Conteudo}</p>
                <button onClick={() => navigate(`/jogo/${guia.jogoId || guia.JogoId}`)}>
                  Ver Jogo →
                </button>
              </div>
            ))}
          </div>
        ) : (
          <p className={styles.semConteudo}>Você ainda não salvou nenhum guia.</p>
        )}
      </section>
    </div>
  );
}