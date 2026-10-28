import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { AutenticacaoAPI } from '../../Services/AutenticacaoAPI';
import { TokenServico } from '../../Services/TokenServico';
import styles from './Login.module.css';

export function Login() {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [erro, setErro] = useState('');
  const [carregando, setCarregando] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErro('');
    setCarregando(true);

    try {
      const resposta = await AutenticacaoAPI.login({ email, senha });
      
      if (resposta.token) {
        TokenServico.salvarToken(resposta.token);
        navigate('/');
      } else {
        setErro('Credenciais inválidas. Tente novamente.');
      }
    } catch (err) {
      console.error('Erro no login:', err);
      setErro('Erro ao realizar login. Verifique seus dados.');
    } finally {
      setCarregando(false);
    }
  };

  return (
    <div className={styles.loginContainer}>
      <form className={styles.formCard} onSubmit={handleSubmit}>
        <h2>Entrar no Projeto Indie 🎮</h2>
        
        {erro && <div className={styles.mensagemErro}>{erro}</div>}

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

        <button type="submit" className={styles.btnSubmit} disabled={carregando}>
          {carregando ? 'Entrando...' : 'Entrar'}
        </button>

        <p className={styles.textoRodape}>
          Ainda não tem conta? <Link to="/registro">Cadastre-se aqui</Link>
        </p>
      </form>
    </div>
  );
}