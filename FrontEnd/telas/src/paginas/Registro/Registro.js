import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { AutenticacaoAPI } from '../../Services/AutenticacaoAPI';
import styles from './Registro.module.css';

export function Registro() {
  const navigate = useNavigate();
  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [confirmarSenha, setConfirmarSenha] = useState('');
  const [erro, setErro] = useState('');
  const [carregando, setCarregando] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErro('');

    if (senha !== confirmarSenha) {
      setErro('As senhas não coincidem.');
      return;
    }

    setCarregando(true);

    try {
      await AutenticacaoAPI.registrar({ nome, email, senha });
      navigate('/login');
    } catch (err) {
      console.error('Erro no registro:', err);
      const mensagemServidor = err.response?.data?.message || err.response?.data || err.message;
      setErro(typeof mensagemServidor === 'string' ? mensagemServidor : 'Erro ao efetuar o cadastro. Verifique os dados informados.');
    } finally {
      setCarregando(false);
    }
  };

  return (
    <div className={styles.registroContainer}>
      <form className={styles.formCard} onSubmit={handleSubmit}>
        <h2>Criar Nova Conta 🚀</h2>

        {erro && <div className={styles.mensagemErro}>{erro}</div>}

        <div className={styles.campo}>
          <label htmlFor="nome">Nome de Usuário</label>
          <input
            id="nome"
            type="text"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            placeholder="Seu nome ou nick"
            required
          />
        </div>

        <div className={styles.campo}>
          <label htmlFor="email">E-mail</label>
          <input
            id="email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="seu@email.com"
            required
          />
        </div>

        <div className={styles.campo}>
          <label htmlFor="senha">Senha</label>
          <input
            id="senha"
            type="password"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
            placeholder="••••••••"
            required
          />
        </div>

        <div className={styles.campo}>
          <label htmlFor="confirmarSenha">Confirmar Senha</label>
          <input
            id="confirmarSenha"
            type="password"
            value={confirmarSenha}
            onChange={(e) => setConfirmarSenha(e.target.value)}
            placeholder="••••••••"
            required
          />
        </div>

        <button type="submit" className={styles.btnSubmit} disabled={carregando}>
          {carregando ? 'Cadastrando...' : 'Cadastrar'}
        </button>

        <p className={styles.textoRodape}>
          Já possui uma conta? <Link to="/login">Faça login</Link>
        </p>
      </form>
    </div>
  );
}