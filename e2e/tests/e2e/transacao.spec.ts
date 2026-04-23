import { test, expect } from '@playwright/test';

test.beforeEach(async ({ page }) => {
  await page.goto('/transacoes');
});

test.describe('Categorias', () => {
    test('[WEB] Verificar se a lista de transações é exibida corretamente', async ({ page }) => {        
        await expect(page.getByRole('columnheader', {name: 'Data'})).toBeVisible();
        await expect(page.getByRole('columnheader', {name: 'Descrição'})).toBeVisible();
        await expect(page.getByRole('columnheader', {name: 'Valor'})).toBeVisible();
        await expect(page.getByRole('columnheader', {name: 'Tipo'})).toBeVisible();
        await expect(page.getByRole('columnheader', {name: 'Categoria'})).toBeVisible();
        await expect(page.getByRole('columnheader', {name: 'Pessoa'})).toBeVisible();
  });
  
    test('[WEB] Verificar se o botão Adicionar Transação abre o modal', async ({ page }) => { 
        await page.getByRole('button', { name: 'Adicionar Transação' }).click();       
        await expect(page.getByLabel('Descrição')).toBeVisible();
        await expect(page.getByLabel('Valor')).toBeVisible();
        await expect(page.getByLabel('Data')).toBeVisible();
        await expect(page.getByLabel('Tipo')).toBeVisible();
        await expect(page.getByLabel('Pessoa')).toBeVisible();
        await expect(page.getByLabel('Categoria')).toBeVisible();        
  });

  test('[WEB] Verificar se é possível criar uma transação de Despesa válida', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Transação' }).click();
    await page.getByRole('textbox', { name: 'Descrição' }).fill('test');
    await page.getByRole('spinbutton', { name: 'Valor' }).fill('100');
    await page.getByRole('textbox', { name: 'Data' }).fill('2000-01-01');
    await page.getByLabel('Tipo').selectOption('despesa');
    await page.getByRole('button', { name: 'Abrir lista' }).first().click();
    await page.getByRole('option', { name: 'josé test' }).nth(2).click();
    await page.getByRole('button', { name: 'Abrir lista' }).nth(1).click();
    await page.getByRole('option', { name: 'Dado Test' }).nth(2).click();
    await page.getByRole('button', { name: 'Salvar' }).click();
    await expect(page.getByText('Transação salva com sucesso!')).toBeVisible();
  });

  test('[WEB] Verificar se é possível criar uma transação de Receita válida', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Transação' }).click();
    await page.getByRole('textbox', { name: 'Descrição' }).fill('test');
    await page.getByRole('spinbutton', { name: 'Valor' }).fill('100');
    await page.getByRole('textbox', { name: 'Data' }).fill('2000-01-01');
    await page.getByLabel('Tipo').selectOption('receita');
    await page.getByRole('button', { name: 'Abrir lista' }).first().click();
    await page.getByRole('option', { name: 'josé test' }).first().click();
    await page.getByRole('button', { name: 'Abrir lista' }).nth(1).click();
    await page.getByRole('option', { name: 'Dado Test' }).first().click();
    await page.getByRole('button', { name: 'Salvar' }).click();
    await expect(page.getByText('Transação salva com sucesso!')).toBeVisible();
  });

  test('[WEB] Verificar se o campo pessoa pesquisa corretamente', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Transação' }).click();    
    await page.getByRole('button', { name: 'Abrir lista' }).first().click();
    await expect(page.getByPlaceholder('Pesquisar pessoas...')).toBeVisible();       
  });

  test('[WEB] Verificar se o campo categoria pesquisa corretamente', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Transação' }).click();
    await page.getByRole('button', { name: 'Abrir lista' }).nth(1).click();
    await expect(page.getByPlaceholder('Pesquisar categorias...')).toBeVisible();
  });

  test('[WEB] Verificar se salvar sem valor exibe erro', async ({ page }) => { 
    await page.getByRole('button', { name: 'Adicionar Transação' }).click();
    await page.getByRole('button', { name: 'Salvar' }).click();   
    await expect(page.getByText('Descrição é obrigatória')).toBeVisible(); 
    await expect(page.getByText('Invalid input: expected number, received NaN')).toBeVisible(); 
    await expect(page.getByText('Invalid input: expected date, received Date')).toBeVisible(); 
    await expect(page.getByText('Invalid input: expected string, received undefined').nth(0)).toBeVisible(); 
    await expect(page.getByText('Invalid input: expected string, received undefined').nth(1)).toBeVisible(); 
  });

  test('[WEB] Verificar paginação na lista de transações', async ({ page }) => { 
    await page.getByRole('button', { name: 'Próximo' }).click();
    await expect(page.getByText('Mostrando 9 - 16 de')).toBeVisible();    
  });
});
