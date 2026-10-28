import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { TopBar } from './components/TopBar';
import { Home } from './paginas/Home/Home';
import { JogoDetalhes } from './paginas/JogoDetalhes/JogoDetalhes';
import { Login } from './paginas/Login/Login';
import { Registro } from './paginas/Registro/Registro';
import { CriarGuia } from './paginas/CriarGuia/CriarGuia';
import { Perfil } from './paginas/Perfil/Perfil';

function App() {
  return (
    <BrowserRouter>
      <TopBar />
      <main style={{ padding: '20px' }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/jogo/:id" element={<JogoDetalhes />} />
          <Route path="/login" element={<Login />} />
          
          <Route path="/registro" element={<Registro />} />
          
          <Route path="/registrar" element={<Navigate to="/registro" replace />} />
          
          <Route path="/guias" element={<Navigate to="/" replace />} />
          
          <Route path="/jogo/:id/novo-guia" element={<CriarGuia />} />
          <Route path="/perfil" element={<Perfil />} />
          
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

export default App;