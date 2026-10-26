import { client } from './Client';

export const GuiaAPI = {
    async obterTodos() {
        const resposta = await client.get('/guia');
        return resposta.data;
    },
    async obterPorId(id) {
        const resposta = await client.get(`/guia/${id}`);
        return resposta.data;
    },
    async cadastrar(guiaCriacaoDto) {
        const resposta = await client.post('/guia', guiaCriacaoDto);
        return resposta.data;
    },
    async atualizar(id, guiaCriacaoDto) {
        const resposta = await client.put(`/guia/${id}`, guiaCriacaoDto);
        return resposta.data;
    },
    async deletar(id) {
        const resposta = await client.delete(`/guia/${id}`);
        return resposta.data;
    }
};