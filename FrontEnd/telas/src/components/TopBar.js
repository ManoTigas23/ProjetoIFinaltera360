import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { TokenServico } from '../../services/TokenServico';
import { AutenticacaoAPI } from '../../services/AutenticacaoAPI';
import './TopBar.css'; 

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
        <Link to="/jogos">Jogos</Link>
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
            <Link to="/registrar" className="btn-registro">Cadastrar</Link>
          </>
        )}
      </div>
    </header>
  );
}