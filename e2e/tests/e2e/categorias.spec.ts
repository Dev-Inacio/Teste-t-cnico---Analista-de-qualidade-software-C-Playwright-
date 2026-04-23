import { test, expect } from '@playwright/test';

test.beforeEach(async ({ page }) => {
  await page.goto('/categorias');
});

test.describe('Categorias', () => {
    test('[WEB] Verificar se a lista de categorias exibe Descrição e Finalidade', async ({ page }) => {        
        await expect(page.getByRole('columnheader', { name: 'Descrição' })).toBeVisible();
        await expect(page.getByRole('columnheader', { name: 'Finalidade' })).toBeVisible();
  });

  test('[WEB] Verificar se o botão Adicionar Categoria abre o modal', async ({ page }) => {        
      await page.getByRole('button', { name: 'Adicionar Categoria' }).click();
      await expect(page.getByLabel('Descrição')).toBeVisible();      
      await expect(page.getByLabel('Finalidade')).toBeVisible();   
  });

  test('[WEB] Verificar se é possível criar categoria do tipo Receita', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Categoria' }).click();
    await page.getByRole('textbox', { name: 'Descrição' }).fill('test');
    await page.getByLabel('Finalidade').selectOption('receita');
    await page.getByRole('button', { name: 'Salvar' }).click();
    await expect(page.getByText('Categoria salva com sucesso!')).toBeVisible();    
  });

  test('[WEB] Verificar se é possível criar categoria do tipo Despesa', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Categoria' }).click();
    await page.getByRole('textbox', { name: 'Descrição' }).fill('test');
    await page.getByLabel('Finalidade').selectOption('despesa');
    await page.getByRole('button', { name: 'Salvar' }).click();
    await expect(page.getByText('Categoria salva com sucesso!')).toBeVisible();    
  });

  test('[WEB] Verificar se é possível criar categoria do tipo Ambas', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Categoria' }).click();
    await page.getByRole('textbox', { name: 'Descrição' }).fill('test');
    await page.getByLabel('Finalidade').selectOption('ambas');
    await page.getByRole('button', { name: 'Salvar' }).click();
    await expect(page.getByText('Categoria salva com sucesso!')).toBeVisible();    
  });

  test('[WEB] Verificar se salvar categoria Sem Descrição exibe erro', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Categoria' }).click();
    await page.getByRole('textbox', { name: 'Descrição' }).fill('');
    await page.getByLabel('Finalidade').selectOption('ambas');
    await page.getByRole('button', { name: 'Salvar' }).click();
    await expect(page.getByText('Descrição é obrigatória')).toBeVisible();    
  });  
});
