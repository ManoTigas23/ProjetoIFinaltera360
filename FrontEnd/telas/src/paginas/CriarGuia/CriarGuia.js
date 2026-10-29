import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { JogoAPI } from '../../Services/JogoAPI';
import { GuiaAPI } from '../../Services/GuiaAPI';
import { TokenServico } from '../../Services/TokenServico';
import styles from './CriarGuia.module.css';

export function CriarGuia() {
  const params = useParams();
  const idDoJogo = params.id || params.jogoId; 
  const navigate = useNavigate();

  const [jogo, setJogo] = useState(null);
  const [titulo, setTitulo] = useState('');
  const [conteudo, setConteudo] = useState('');
  const [videoYoutube, setVideoYoutube] = useState('');
  const [erro, setErro] = useState('');
  const [carregando, setCarregando] = useState(false);

  useEffect(() => {
    const estaAutenticado = TokenServico.usuarioAutenticado ? TokenServico.usuarioAutenticado() : TokenServico.obterToken() !== null;
    if (!estaAutenticado) {
      navigate('/login');
      return;
    }

    async function carregarJogo() {
      if (!idDoJogo) return;
      try {
        const idNum = parseInt(idDoJogo, 10);
        const dadosJogo = await JogoAPI.obterPorId(idNum);
        setJogo(dadosJogo);
      } catch (err) {
        console.error('Erro ao buscar jogo:', err);
        setErro('Não foi possível carregar os dados do jogo.');
      }
    }

    carregarJogo();
  }, [idDoJogo, navigate]);

 const handleSubmit = async (e) => {
  e.preventDefault();
  setErro('');
  setCarregando(true);

  try {
    const idJogoNum = parseInt(idDoJogo, 10);
    
    const usuarioIdNum = TokenServico.obterUsuarioId 
      ? parseInt(TokenServico.obterUsuarioId(), 10) 
      : parseInt(localStorage.getItem('usuarioId') || '1', 10); 

    const guiaCriacaoDto = {
      titulo: titulo,
      conteudo: conteudo,
      usuarioId: usuarioIdNum,
      jogoId: idJogoNum,
      videoYoutube: videoYoutube || ''
    };

    await GuiaAPI.cadastrar(guiaCriacaoDto);
    navigate(`/jogo/${idJogoNum}`);
  } catch (err) {
    console.error('Erro ao publicar guia:', err.response?.data || err);
    setErro('Erro ao publicar o guia. Verifique se está autenticado e tente novamente.');
  } finally {
    setCarregando(false);
  }
};
  const nomeExibicaoJogo = jogo?.nomeJogo || jogo?.NomeJogo || jogo?.nome || jogo?.titulo || 'Jogo';

  return (
    <div className={styles.criarGuiaContainer}>
      <button 
        type="button"
        className={styles.btnVoltar} 
        onClick={() => navigate(`/jogo/${idDoJogo}`)}
      >
        ← Voltar para o Jogo
      </button>

      <div className={styles.formCard}>
        <h2>Criar Novo Guia para {nomeExibicaoJogo} 📝</h2>

        {erro && <div className={styles.mensagemErro}>{erro}</div>}

        <form onSubmit={handleSubmit}>
          <div className={styles.campo}>
            <label htmlFor="titulo">Título do Guia</label>
            <input
              id="titulo"
              type="text"
              value={titulo}
              onChange={(e) => setTitulo(e.target.value)}
              placeholder="Ex: Como derrotar o primeiro chefe / Localização dos itens"
              required
            />
          </div>

          <div className={styles.campo}>
            <label htmlFor="conteudo">Conteúdo / Tutorial</label>
            <textarea
              id="conteudo"
              rows="8"
              value={conteudo}
              onChange={(e) => setConteudo(e.target.value)}
              placeholder="Escreva passo a passo o seu guia aqui..."
              required
            />
          </div>

          <div className={styles.campo}>
            <label htmlFor="videoYoutube">URL de Vídeo do YouTube (Opcional)</label>
            <input
              id="videoYoutube"
              type="url"
              value={videoYoutube}
              onChange={(e) => setVideoYoutube(e.target.value)}
              placeholder="https://www.youtube.com/watch?v=..."
            />
          </div>

          <button 
            type="submit" 
            className={styles.btnSubmit} 
            disabled={carregando}
          >
            {carregando ? 'Publicando...' : 'Publicar Guia'}
          </button>
        </form>
      </div>
    </div>
  );
}