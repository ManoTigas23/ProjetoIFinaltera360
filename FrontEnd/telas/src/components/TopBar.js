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
    <header className="topbar">
      <div className="topbar-logo">
        <Link to="/">🎮 ProjetoIndie</Link>
      </div>

      <nav className="topbar-nav">
      <Link to="/" className="btn-jogos">Jogos</Link>
      <Link to="/guias">Guias</Link>        
      </nav>

      <div className="topbar-user">
        {estaAutenticado ? (
          <>
            <button 
              className="btn-perfil" 
              onClick={() => navigate('/perfil')}
            >
              👤 Meu Perfil
            </button>
            <button 
              className="btn-logout" 
              onClick={handleLogout}
            >
              Sair
            </button>
          </>
        ) : (
          <>
            <Link to="/login" className="btn-login">Entrar</Link>
            <Link to="/registro" className="btn-registro">Cadastrar</Link>           
          </>
        )}
      </div>
    </header>
  );
}