const TOKEN_KEY = '@App:token';

export const TokenServico = {
    obterToken() {
        return localStorage.getItem(TOKEN_KEY);
    },
    salvarToken(token) {
        localStorage.setItem(TOKEN_KEY, token);
    },
    removerToken() {
        localStorage.removeItem(TOKEN_KEY);
    },
    usuarioAutenticado() {
        return !!localStorage.getItem(TOKEN_KEY);
    }
};