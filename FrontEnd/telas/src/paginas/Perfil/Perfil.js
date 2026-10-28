import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { TokenServico } from '../../Services/TokenServico';
import { GuiaAPI } from '../../Services/GuiaAPI';
import styles from './Perfil.module.css';

export function Perfil() {
  const navigate = useNavigate();
  const [meusGuias, setMeusGuias] = useState([]);
  const [carregando, setCarregando] = useState(true);

  useEffect(() => {
    if (!TokenServico.usuarioAutenticado()) {
      navigate('/login');
      return;
    }

    async function carregarDadosPerfil() {
      try {
        setCarregando(true);
        const todosGuias = await GuiaAPI.obterTodos();
        
        const idUsuarioLogado = TokenServico.obterUsuarioId();
        const guiasDoUsuario = todosGuias.filter(
          (g) => g.usuarioId === idUsuarioLogado || g.usuarioID === idUsuarioLogado
        );

        setMeusGuias(guiasDoUsuario);
      } catch (erro) {
        console.error('Erro ao carregar dados do perfil:', erro);
      } finally {
        setCarregando(false);
      }
    }

    carregarDadosPerfil();
  }, [navigate]);

  const handleLogout = () => {
    TokenServico.removerToken();
    navigate('/login');
  };

  if (carregando) {
    return (
      <div className={styles.perfilContainer}>
        <h3>A carregar informações do perfil...</h3>
      </div>
    );
  }

  return (
    <div className={styles.perfilContainer}>
      <header className={styles.cabecalhoPerfil}>
        <div className={styles.infoUsuario}>
          <h2>Meu Perfil 👤</h2>
          <p>Gerencie as suas publicações e conta</p>
        </div>
        <button className={styles.btnSair} onClick={handleLogout}>
          Sair da Conta
        </button>
      </header>

      <section className={styles.seccaoGuias}>
        <h3>Meus Guias Publicados ({meusGuias.length})</h3>

        <div className={styles.listaGuias}>
          {meusGuias.length > 0 ? (
            meusGuias.map((guia) => (
              <article key={guia.guiaId || guia.id} className={styles.guiaCard}>
                <h4>{guia.titulo}</h4>
                <p>{guia.conteudo}</p>
                {guia.videoYoutube && (
                  <a
                    href={guia.videoYoutube}
                    target="_blank"
                    rel="noreferrer"
                    className={styles.linkVideo}
                  >
                    📺 Ver Vídeo
                  </a>
                )}
              </article>
            ))
          ) : (
            <div className={styles.semGuias}>
              <p>Você ainda não publicou nenhum guia.</p>
            </div>
          )}
        </div>
      </section>
    </div>
  );
}