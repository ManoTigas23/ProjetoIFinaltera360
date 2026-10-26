import { client } from './Client';

export const ComentarioAPI = {
    async obterTodos() {
        const resposta = await client.get('/comentario');
        return resposta.data;
    },
    async obterPorId(id) {
        const resposta = await client.get(`/comentario/${id}`);
        return resposta.data;
    },
    async cadastrar(comentarioCriacaoDto) {
        const resposta = await client.post('/comentario', comentarioCriacaoDto);
        return resposta.data;
    },
    async atualizar(id, comentarioCriacaoDto) {
        const resposta = await client.put(`/comentario/${id}`, comentarioCriacaoDto);
        return resposta.data;
    },
    async deletar(id) {
        const resposta = await client.delete(`/comentario/${id}`);
        return resposta.data;
    }
};