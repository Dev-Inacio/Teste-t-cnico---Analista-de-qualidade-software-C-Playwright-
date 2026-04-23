import { test, expect } from '@playwright/test';

test.beforeEach(async ({ page }) => {
  await page.goto('/pessoas');
});

test.describe('Pessoas', () => {
    test('[WEB] Verificar se a lista de pessoas é exibida corretamente', async ({ page }) => {

        await expect (page.getByRole('columnheader',{name: 'Nome'})).toBeVisible();
        await expect (page.getByRole('columnheader',{name: 'Data de Nascimento'})).toBeVisible();
        await expect (page.getByRole('columnheader',{name: 'Idade'})).toBeVisible();
        await expect (page.getByRole('columnheader',{name: 'Ações'})).toBeVisible();      
  });

  test('[WEB] Verificar se o botão Adicionar Pessoa abre o modal', async ({ page }) => {
    await page.getByRole('button', { name: 'Adicionar Pessoa' }).click();   
    await expect(page.getByRole('textbox',{name: 'Nome'})).toBeVisible(); 
    await expect(page.getByRole('textbox',{name: 'Data de Nascimento'})).toBeVisible();             
  });

  test('[WEB] Verificar se é possível cadastrar uma pessoa com dados válidos', async ({ page }) => {
    await page.getByRole('button', { name: 'Adicionar Pessoa' }).click();
    await page.getByRole('textbox', { name: 'Nome' }).fill('João Teste');    
    await page.getByRole('textbox', { name: 'Data de Nascimento' }).fill('2000-01-01');
    await page.getByRole('button', { name: 'Salvar' }).click();   
    await expect(page.getByText('Pessoa salva com sucesso')).toBeVisible();             
  });

  test('[WEB] Verificar se o botão Editar abre o modal com dados preenchidos', async ({ page }) => {
    await page.getByRole('button', { name: 'Editar' }).first().click(); 
    await expect(page.getByRole('textbox',{name: 'Nome'})).not.toHaveValue(''); 
    await expect(page.getByRole('textbox',{name: 'Data de Nascimento'})).not.toHaveValue('');             
  });

  test('[WEB] Verificar se é possível deletar uma pessoa', async ({ page }) => {
    await page.getByRole('button', { name: 'Deletar' }).first().click();
    await page.getByRole('button', { name: 'Confirmar' }).click();      
    await expect(page.getByRole('button', { name: 'Confirmar' })).not.toBeVisible();           
  });
  
  test('[WEB] Verificar se salvar com nome vazio exibe erro', async ({ page }) => {
    await page.getByRole('button', { name: 'Adicionar Pessoa' }).click();
    await page.getByRole('textbox', { name: 'Nome' }).fill('');    
    await page.getByRole('textbox', { name: 'Data de Nascimento' }).fill('2000-01-01');
    await page.getByRole('button', { name: 'Salvar' }).click();   
    await expect(page.getByText('Nome é obrigatório')).toBeVisible();             
  });

  test('[WEB] Verificar se salvar com nome Data de Nascimento Vazia exibe erro', async ({ page }) => {
    await page.getByRole('button', { name: 'Adicionar Pessoa' }).click();
    await page.getByRole('textbox', { name: 'Nome' }).fill('nome test');    
    await page.getByRole('textbox', { name: 'Data de Nascimento' }).fill('');
    await page.getByRole('button', { name: 'Salvar' }).click();   
    await expect(page.getByText('Invalid input: expected date')).toBeVisible();             
  });

  test('[WEB] Verificar se o botão Cancelar fecha o modal sem salvar', async ({ page }) => {
    await page.getByRole('button', { name: 'Adicionar Pessoa' }).click();
    await page.getByRole('textbox', { name: 'Nome' }).fill('João Teste');    
    await page.getByRole('textbox', { name: 'Data de Nascimento' }).fill('2000-01-01');
    await page.getByRole('button', { name: 'Cancelar' }).click();   
    await expect(page.getByText('Pessoa salva com sucesso')).not.toBeVisible();              
  });

  test('[WEB] Verificar paginação na lista de pessoas', async ({ page }) => {
    await page.getByRole('button', { name: 'Próximo' }).click();
    await expect(page.getByText('Mostrando 9 - 16 de')).toBeVisible();
  });
});