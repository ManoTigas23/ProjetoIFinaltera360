import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { TokenServico } from '../Services/TokenServico';
import { AutenticacaoAPI } from '../Services/AutenticacaoAPI';
import styles from './TopBar.module.css';

export function TopBar() {
  const navigate = useNavigate();
  const estaAutenticado = TokenServico.usuarioAutenticado();

  const handleLogout = () => {
    AutenticacaoAPI.logout();
    navigate('/login');
  };

  return (
    <header className={styles.topbar}>
      <div className={styles.topbarLogo}>
        <Link to="/">🎮 ProjetoIndie</Link>
      </div>

      <nav className={styles.topbarNav}>
        <Link to="/" className={styles.btnJogos}>Jogos</Link>
      </nav>

      <div className={styles.topbarUser}>
        {estaAutenticado ? (
          <>
            <button
              className={styles.btnPerfil}
              onClick={() => navigate('/perfil')}
            >
              👤 Meu Perfil
            </button>
            <button
              className={styles.btnLogout}
              onClick={handleLogout}
            >
              Sair
            </button>
          </>
        ) : (
          <>
            <Link to="/login" className={styles.btnLogin}>Entrar</Link>
            <Link to="/registro" className={styles.btnRegistro}>Cadastrar</Link>
          </>
        )}
      </div>
    </header>
  );
}