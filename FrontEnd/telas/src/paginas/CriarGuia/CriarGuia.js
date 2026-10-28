import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { JogoAPI } from '../../Services/JogoAPI';
import { GuiaAPI } from '../../Services/GuiaAPI';
import { TokenServico } from '../../Services/TokenServico';
import styles from './CriarGuia.module.css';

export function CriarGuia() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [jogo, setJogo] = useState(null);
  const [titulo, setTitulo] = useState('');
  const [conteudo, setConteudo] = useState('');
  const [videoYoutube, setVideoYoutube] = useState('');
  const [erro, setErro] = useState('');
  const [carregando, setCarregando] = useState(false);

  useEffect(() => {
    if (!TokenServico.estaAutenticado()) {
      navigate('/login');
      return;
    }

    async function carregarJogo() {
      try {
        const dadosJogo = await JogoAPI.obterPorId(parseInt(id, 10));
        setJogo(dadosJogo);
      } catch (err) {
        console.error('Erro ao buscar jogo:', err);
        setErro('Não foi possível carregar os dados do jogo.');
      }
    }

    carregarJogo();
  }, [id, navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErro('');
    setCarregando(true);

    try {
      const novoGuia = {
        jogoId: parseInt(id, 10),
        titulo,
        conteudo,
        videoYoutube: videoYoutube || null
      };

      await GuiaAPI.criar(novoGuia);
      navigate(`/jogo/${id}`);
    } catch (err) {
      console.error('Erro ao criar guia:', err);
      setErro('Erro ao publicar o guia. Tente novamente.');
    } finally {
      setCarregando(false);
    }
  };

  return (
    <div className={styles.criarGuiaContainer}>
      <button className={styles.btnVoltar} onClick={() => navigate(`/jogo/${id}`)}>
        ← Voltar para o Jogo
      </button>

      <div className={styles.formCard}>
        <h2>Criar Novo Guia {jogo ? `para ${jogo.nome || jogo.titulo}` : ''} 📝</h2>

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

          <button type="submit" className={styles.btnSubmit} disabled={carregando}>
            {carregando ? 'Publicando...' : 'Publicar Guia'}
          </button>
        </form>
      </div>
    </div>
  );
}