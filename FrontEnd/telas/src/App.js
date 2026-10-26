import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { TopBar } from './componentes/TopBar';

function App() {
  return (
    <BrowserRouter>
      <TopBar />

      <main style={{ padding: '20px' }}>
        <Routes>
          <Route path="/" element={<h1>Página Inicial</h1>} />
          <Route path="/perfil" element={<h1>Tela do Usuário</h1>} />
          <Route path="/login" element={<h1>Tela de Login</h1>} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

export default App;